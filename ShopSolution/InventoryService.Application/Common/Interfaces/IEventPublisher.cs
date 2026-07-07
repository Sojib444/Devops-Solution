namespace InventoryService.Application.Common.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(string routingKey, T message);
}
