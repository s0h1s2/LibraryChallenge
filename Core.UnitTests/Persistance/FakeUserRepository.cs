using Core.Entity;
using Core.Persistance;

namespace Core.UnitTests.Persistance;

public class FakeUserRepository : IUserRepository
{
    public List<User> Users { get; }
    public Task<User> CreateUserAsync(User user)
    {
        Users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> GetUserByIdAsync(Guid userId)
    {
        return Task.FromResult(User.Create("shkar", "password", 1));
    }

    public Task<User> GetUserWithBorrowedBooksAsync(Guid userId, Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User? user)
    {
        throw new NotImplementedException();
    }
}