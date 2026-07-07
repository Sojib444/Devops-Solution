using System.Text;
using System.Text.Json;
using InventoryService.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopSolution.Shared.Events;

namespace InventoryService.Infrastructure.Messaging.Consumers;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly IEventPublisher _publisher;

    public OrderCreatedConsumer(IServiceProvider serviceProvider, IEventPublisher publisher)
    {
        _serviceProvider = serviceProvider;
        _publisher = publisher;

        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var rabbitHost = config["RabbitMq:Host"] ?? "localhost";

        var factory = new ConnectionFactory { HostName = rabbitHost };
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        _channel.ExchangeDeclareAsync("ecommerce", ExchangeType.Direct).GetAwaiter().GetResult();
        _channel.QueueDeclareAsync("inventory-order-created", durable: true, exclusive: false, autoDelete: false).GetAwaiter().GetResult();
        _channel.QueueBindAsync("inventory-order-created", "ecommerce", "order.created").GetAwaiter().GetResult();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(json);
            if (orderEvent is null) return;

            using var scope = _serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();

            try
            {
                foreach (var item in orderEvent.Items)
                {
                    var product = await repo.GetByIdAsync(item.ProductId);
                    if (product is null)
                    {
                        await _publisher.PublishAsync("stock.reserved", new StockReservedEvent
                        {
                            OrderId = orderEvent.OrderId,
                            Success = false,
                            Message = $"Product {item.ProductId} not found"
                        });
                        return;
                    }

                    try
                    {
                        product.ReduceStock(item.Quantity);
                        await repo.UpdateAsync(product);
                    }
                    catch (InvalidOperationException)
                    {
                        await _publisher.PublishAsync("stock.reserved", new StockReservedEvent
                        {
                            OrderId = orderEvent.OrderId,
                            Success = false,
                            Message = $"Insufficient stock for {product.Name}"
                        });
                        return;
                    }
                }

                await _publisher.PublishAsync("stock.reserved", new StockReservedEvent
                {
                    OrderId = orderEvent.OrderId,
                    Success = true,
                    Message = "Stock reserved"
                });
            }
            catch (Exception ex)
            {
                await _publisher.PublishAsync("stock.reserved", new StockReservedEvent
                {
                    OrderId = orderEvent.OrderId,
                    Success = false,
                    Message = ex.Message
                });
            }
        };

        await _channel.BasicConsumeAsync("inventory-order-created", autoAck: true, consumer: consumer);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
