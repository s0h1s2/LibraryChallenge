using Core.Entity;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistence.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _dbContext.User
            .FirstAsync(u => u.Id == userId);
    }

    // This is one is better if you want to get the user with borrowed books filtered by bookId
    // Performance wises, it will only load the borrowed books that match the bookId
    public async Task<User> GetUserWithBorrowedBooksAsync(Guid userId, Guid bookId)
    {
        return await _dbContext.User
            .Include(user => user.BorrowedBooks.Where(bb => bb.BookId == bookId))
            .FirstAsync(u => u.Id == userId);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _dbContext.User.Update(user);
        _dbContext.BorrowedBooks.UpdateRange(user.BorrowedBooks.Select(br => br));
        await _dbContext.SaveChangesAsync();
        return user;
    }
}