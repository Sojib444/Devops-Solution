using InventoryService.Domain.Entities;

namespace InventoryService.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task UpdateAsync(Product product);
}
