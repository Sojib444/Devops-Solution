namespace ShopSolution.Shared.Events;

public record OrderConfirmedEvent
{
    public int OrderId { get; init; }
    public string CustomerEmail { get; init; } = string.Empty;
}
