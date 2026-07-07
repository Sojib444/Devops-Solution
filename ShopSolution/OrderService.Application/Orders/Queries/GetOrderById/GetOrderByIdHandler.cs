using MediatR;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Orders.Common;

namespace OrderService.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order is null) return null;

        return Map(order);
    }

    private static OrderDto Map(Domain.Entities.Order order) => new(
        order.Id,
        order.CustomerEmail,
        order.TotalAmount,
        order.Status,
        order.CreatedAt,
        order.Items.Select(i => new OrderItemDto(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList());
}
