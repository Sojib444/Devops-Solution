using InventoryService.Application.Common.Interfaces;
using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;

    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}
