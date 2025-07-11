using Core;
using Core.Dto;
using Core.Entity;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistence.Implementation;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> AddBookAsync(CreateBook book)
    {
        var bookToAdd = Book.CreateBook(isbn: book.Isbn, title: book.Title, category: new CategoryId(book.CategoryId),
            author: book.Author, availableCopies: book.AvailableCopies);
        _context.Books.Add(bookToAdd);
        await _context.SaveChangesAsync();
        return bookToAdd;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        var bookEntity = await _context.Books.FindAsync(book.Id);
        if (bookEntity == null)
        {
            throw new KeyNotFoundException($"Book with id {book.Id} not found.");
        }

        _context.Books.Update(bookEntity);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<IList<Book>> GetBooksAsync()
    {
        var books = await _context.Books.ToListAsync();
        return books
            .Select(book =>
                Book.CreateExisting(
                    book.Id,
                    book.Isbn,
                    book.Title,
                    book.CategoryId,
                    book.Author,
                    book.AvailableCopies
                ))
            .ToList();
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _context.Books.FindAsync(id);
        if (bookEntity == null)
        {
            return null;
        }

        return bookEntity;
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found.");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }

    public async Task<IEnumerable<Book>> FilterBooksAsync(string searchTerm)
    {
        var books = await _context.Books.Where(book =>
                book.Title.Contains(searchTerm) || book.Author.Contains(searchTerm) || book.Isbn.Equals(searchTerm))
            .ToListAsync();

        return books;
    }

    public async Task UpdateBookAndBorrowedBooksByUserAsync(Book book, User user)
    {
        _context.Books.Update(book);
        _context.BorrowedBooks.UpdateRange(user.BorrowedBooks);
        await _context.SaveChangesAsync();
    }
}