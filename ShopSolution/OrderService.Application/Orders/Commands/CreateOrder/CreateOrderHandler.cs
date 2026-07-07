using MediatR;
using OrderService.Application.Common.Interfaces;
using OrderService.Domain.Entities;
using ShopSolution.Shared.Events;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _eventPublisher;

    public CreateOrderHandler(IOrderRepository orderRepository, IEventPublisher eventPublisher)
    {
        _orderRepository = orderRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.Items.Select(i => new OrderItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList();
        var order = new Order(request.CustomerEmail, items);

        var created = await _orderRepository.AddAsync(order);

        await _eventPublisher.PublishAsync("order.created", new OrderCreatedEvent
        {
            OrderId = created.Id,
            Items = request.Items.Select(i => new OrderItemEvent
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        });

        return created.Id;
    }
}
