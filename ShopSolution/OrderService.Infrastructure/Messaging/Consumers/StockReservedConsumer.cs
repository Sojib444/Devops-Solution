using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Application.Common.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopSolution.Shared.Events;

namespace OrderService.Infrastructure.Messaging.Consumers;

public class StockReservedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public StockReservedConsumer(IServiceProvider serviceProvider, IEventPublisher publisher)
    {
        _serviceProvider = serviceProvider;
        var config = serviceProvider.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
        var rabbitHost = config["RabbitMq:Host"] ?? "localhost";

        var factory = new ConnectionFactory { HostName = rabbitHost };
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        _channel.ExchangeDeclareAsync("ecommerce", ExchangeType.Direct).GetAwaiter().GetResult();
        _channel.QueueDeclareAsync("order-stock-reserved", durable: true, exclusive: false, autoDelete: false).GetAwaiter().GetResult();
        _channel.QueueBindAsync("order-stock-reserved", "ecommerce", "stock.reserved").GetAwaiter().GetResult();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var result = JsonSerializer.Deserialize<StockReservedEvent>(json);
            if (result is null) return;

            using var scope = _serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

            var order = await repo.GetByIdAsync(result.OrderId);
            if (order is null) return;

            if (result.Success)
                order.Confirm();
            else
                order.Fail();

            await repo.UpdateAsync(order);

            if (result.Success)
            {
                await publisher.PublishAsync("order.confirmed", new OrderConfirmedEvent
                {
                    OrderId = order.Id,
                    CustomerEmail = order.CustomerEmail
                });
            }
        };

        await _channel.BasicConsumeAsync("order-stock-reserved", autoAck: true, consumer: consumer);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
