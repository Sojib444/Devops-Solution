namespace OrderService.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public string CustomerEmail { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = "Pending";
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; private set; } = [];

    private Order() { }

    public Order(string customerEmail, List<OrderItem> items)
    {
        CustomerEmail = customerEmail;
        Items = items;
        TotalAmount = items.Sum(i => i.Quantity * i.UnitPrice);
    }

    public void Confirm()
    {
        Status = "Confirmed";
    }

    public void Fail()
    {
        Status = "Failed";
    }
}
