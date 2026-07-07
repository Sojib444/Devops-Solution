using Microsoft.EntityFrameworkCore;
using OrderService.Application.Common.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order> AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.Include(o => o.Items).ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
