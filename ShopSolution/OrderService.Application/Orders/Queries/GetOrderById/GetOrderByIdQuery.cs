using MediatR;
using OrderService.Application.Orders.Common;

namespace OrderService.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;
