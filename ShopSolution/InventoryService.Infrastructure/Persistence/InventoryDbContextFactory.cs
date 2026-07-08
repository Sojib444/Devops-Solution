using InventoryService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryService.Infrastructure.Persistence;

public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=InventoryServiceDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True;");
        return new InventoryDbContext(optionsBuilder.Options);
    }
}
