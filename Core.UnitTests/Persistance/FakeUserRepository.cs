using Core.Entity;
using Core.Persistance;

namespace Core.UnitTests.Persistance;

public class FakeUserRepository:IUserRepository
{   
    public List<User> Users { get; }
    public Task<User> CreateUserAsync(User user)
    {
        Users.Add(user);
        return Task.FromResult(user);
    }
}