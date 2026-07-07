using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerEmail).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(50).IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId);
    }
}
