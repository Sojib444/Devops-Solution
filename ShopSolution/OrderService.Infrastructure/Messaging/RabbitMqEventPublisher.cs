using System.Text;
using System.Text.Json;
using OrderService.Application.Common.Interfaces;
using RabbitMQ.Client;

namespace OrderService.Infrastructure.Messaging;

public class RabbitMqEventPublisher : IEventPublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqEventPublisher(string hostname)
    {
        var factory = new ConnectionFactory { HostName = hostname };
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        _channel.ExchangeDeclareAsync("ecommerce", ExchangeType.Direct).GetAwaiter().GetResult();
    }

    public async Task PublishAsync<T>(string routingKey, T message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        await _channel.BasicPublishAsync("ecommerce", routingKey, body);
    }

    public void Dispose()
    {
        _channel?.CloseAsync();
        _connection?.CloseAsync();
    }
}
