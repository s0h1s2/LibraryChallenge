using Core.Entity;
using Core.Persistance;

namespace Web.Persistance;

public class UserRepository:IUserRepository
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

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _dbContext.User.FindAsync(userId);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _dbContext.User.Update(user);
        _dbContext.BorrowedBooks.UpdateRange(user.BorrowedBooks.Select(br=>br));
        await _dbContext.SaveChangesAsync();
        return user;
    }
}