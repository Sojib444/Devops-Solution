using OrderService.Domain.Entities;

namespace OrderService.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<List<Order>> GetAllAsync();
    Task UpdateAsync(Order order);
}
