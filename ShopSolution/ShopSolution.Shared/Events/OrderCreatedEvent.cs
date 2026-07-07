namespace ShopSolution.Shared.Events;

public record OrderCreatedEvent
{
    public int OrderId { get; init; }
    public List<OrderItemEvent> Items { get; init; } = [];
}

public record OrderItemEvent
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
