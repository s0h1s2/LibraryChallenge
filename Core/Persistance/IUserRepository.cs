using Core.Entity;

namespace Core.Persistance;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
}