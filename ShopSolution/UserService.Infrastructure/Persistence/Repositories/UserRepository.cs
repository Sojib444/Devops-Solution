using Microsoft.EntityFrameworkCore;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);

        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);

        return _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null)
    {
        return await _context.Users
            .AnyAsync(x => x.Email == email && (!excludeId.HasValue || x.Id != excludeId.Value));
    }
}