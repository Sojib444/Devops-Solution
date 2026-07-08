using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderService.Infrastructure.Persistence;

public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=OrderServiceDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True;");
        return new OrderDbContext(optionsBuilder.Options);
    }
}
