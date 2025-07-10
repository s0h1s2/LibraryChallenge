using Core.Entity;

namespace Core.Persistance;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserWithBorrowedBooksAsync(Guid userId,Guid bookId);
    Task<User> UpdateUserAsync(User? user);
}