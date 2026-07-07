using MediatR;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string CustomerEmail,
    List<CreateOrderItemDto> Items) : IRequest<int>;

public record CreateOrderItemDto(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice);
