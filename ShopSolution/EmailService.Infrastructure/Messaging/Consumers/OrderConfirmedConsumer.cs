using System.Text;
using System.Text.Json;
using EmailService.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopSolution.Shared.Events;

namespace EmailService.Infrastructure.Messaging.Consumers;

public class OrderConfirmedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public OrderConfirmedConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var rabbitHost = config["RabbitMq:Host"] ?? "localhost";

        var factory = new ConnectionFactory { HostName = rabbitHost };
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        _channel.ExchangeDeclareAsync("ecommerce", ExchangeType.Direct).GetAwaiter().GetResult();
        _channel.QueueDeclareAsync("email-order-confirmed", durable: true, exclusive: false, autoDelete: false).GetAwaiter().GetResult();
        _channel.QueueBindAsync("email-order-confirmed", "ecommerce", "order.confirmed").GetAwaiter().GetResult();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var evt = JsonSerializer.Deserialize<OrderConfirmedEvent>(json);
            if (evt is null) return;

            using var scope = _serviceProvider.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
            await sender.SendOrderConfirmationAsync(evt.CustomerEmail, evt.OrderId);
        };

        await _channel.BasicConsumeAsync("email-order-confirmed", autoAck: true, consumer: consumer);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
