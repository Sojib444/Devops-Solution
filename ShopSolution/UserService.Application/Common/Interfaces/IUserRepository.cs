using UserService.Domain.Entities;

namespace UserService.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);

    Task<User?> GetByIdAsync(int id);

    Task<bool> ExistsByEmailAsync(string email);
}