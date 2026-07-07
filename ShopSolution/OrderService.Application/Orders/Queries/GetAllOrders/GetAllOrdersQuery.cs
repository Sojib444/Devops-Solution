using MediatR;
using OrderService.Application.Orders.Common;

namespace OrderService.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<List<OrderDto>>;
