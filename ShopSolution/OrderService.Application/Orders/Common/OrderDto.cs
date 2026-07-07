namespace OrderService.Application.Orders.Common;

public record OrderDto(
    int Id,
    string CustomerEmail,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    List<OrderItemDto> Items);

public record OrderItemDto(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice);
