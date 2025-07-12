using Core.Dto;
using Core.Entity;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Web.Persistence;

namespace Web.Services;

public class BookService
{
    private readonly LibraryService _libraryService;
    private ApplicationDbContext _dbContext;

    public BookService(LibraryService libraryService, ApplicationDbContext dbContext)
    {
        _libraryService = libraryService;
        _dbContext = dbContext;
    }

    public async Task<Book?> AddBookAsync(CreateBook bookToAdd)
    {
        var book = _dbContext.Books.Add(bookToAdd.ToBook());
        await _dbContext.SaveChangesAsync();
        return book.Entity;
    }

    public async Task BorrowBookAsync(BorrowBook borrowBook, Guid userId, DateTime dueDate)
    {
        var book = await _dbContext.Books.FindAsync(borrowBook.BookId);
        var user = await _dbContext.User.Include(user => user.BorrowedBooks).FirstAsync(user => user.Id == userId);
        if (book is null || user is null) throw new KeyNotFoundException();

        _libraryService.BorrowBook(book, user, dueDate);
        _dbContext.Add(book);
        _dbContext.Add(user.BorrowedBooks);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(Guid bookId, Guid userId)
    {
        var book = await _dbContext.Books.FindAsync(bookId);
        var user = await _dbContext.User.Include(user => user.BorrowedBooks).FirstAsync(user => user.Id == userId);
        if (book is null || user is null) throw new KeyNotFoundException();
        _libraryService.ReturnBook(book, user);
        _dbContext.Books.Update(book);
        _dbContext.BorrowedBooks.UpdateRange(user.BorrowedBooks);
        await _dbContext.SaveChangesAsync();
    }
}