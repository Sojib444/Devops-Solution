//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace UserService.Infrastructure.Persistence;

//public class ApplicationDbContextFactory
//    : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder =
//            new DbContextOptionsBuilder<ApplicationDbContext>();

//        optionsBuilder.UseSqlServer(
//            "Server=localhost;Database=UserServiceDb;Trusted_Connection=True;TrustServerCertificate=True;");

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }
//}