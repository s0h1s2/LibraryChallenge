using Core;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance;

public class BookRepository:IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<Book?> AddBookAsync(Book book)
    {
        return null;
    }

    public async Task<IList<Book>> GetBooksAsync()
    {
        var books = await _context.Books.ToListAsync();
        return books
            .Select(
                book=>
                    Book.CreateExisting(
                        book.Id,
                        book.Isbn,
                        book.Title,
                        new CategoryId(book.CategoryId),
                        book.Author,
                        book.AvailableCopies
                    ))
            .ToList();
    }
}