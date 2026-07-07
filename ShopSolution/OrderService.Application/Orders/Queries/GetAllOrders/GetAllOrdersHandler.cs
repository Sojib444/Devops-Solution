using MediatR;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Orders.Common;

namespace OrderService.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync();

        return orders.Select(order => new OrderDto(
            order.Id,
            order.CustomerEmail,
            order.TotalAmount,
            order.Status,
            order.CreatedAt,
            order.Items.Select(i => new OrderItemDto(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)).ToList()
        )).ToList();
    }
}
