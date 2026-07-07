namespace ShopSolution.Shared.Events;

public record StockReservedEvent
{
    public int OrderId { get; init; }
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
}
