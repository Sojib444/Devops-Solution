namespace InventoryService.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    private Product() { }

    public Product(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));

        if (StockQuantity < quantity)
            throw new InvalidOperationException($"Insufficient stock. Available: {StockQuantity}, Requested: {quantity}");

        StockQuantity -= quantity;
    }
}
