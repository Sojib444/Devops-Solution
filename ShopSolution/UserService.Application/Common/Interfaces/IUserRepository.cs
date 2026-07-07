using UserService.Domain.Entities;

namespace UserService.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);

    Task<User?> GetByIdAsync(int id);

    Task<List<User>> GetAllAsync();

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);

    Task<bool> ExistsByEmailAsync(string email, int? excludeId = null);
}