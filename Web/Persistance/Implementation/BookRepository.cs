using Core;
using Core.Dto;
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
    public async Task<Book?> AddBookAsync(CreateBook book)
    {
        var bookToAdd = new BookEntity()
        {
            Isbn = book.Isbn,
            Author = book.Author,
            Title = book.Title,
            CategoryId = book.CategoryId,
            AvailableCopies = book.AvailableCopies
        };
        _context.Books.Add(bookToAdd);
        await _context.SaveChangesAsync();
        return Book.CreateExisting(
            bookToAdd.Id,
            book.Isbn,
            book.Title,
            new CategoryId(bookToAdd.CategoryId), 
            book.Author, 
            book.AvailableCopies
            );
        
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        var bookEntity = _context.Books.Find(book.Id);
        if (bookEntity == null)
        {
            throw new KeyNotFoundException($"Book with id {book.Id} not found.");
        }

        bookEntity.Isbn = book.Isbn;
        bookEntity.Title = book.Title;
        bookEntity.Author = book.Author;
        bookEntity.CategoryId = book.CategoryId.Id;
        bookEntity.AvailableCopies = book.AvailableCopies;
        _context.Books.Update(bookEntity);
        await _context.SaveChangesAsync();
        return book;
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

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        var bookEntity=await _context.Books.FindAsync(id);
        if (bookEntity == null)
        {
            return null;
        }

        return Book.CreateExisting(
            bookEntity.Id,
            bookEntity.Isbn,
            bookEntity.Title,
            new CategoryId(bookEntity.CategoryId),
            bookEntity.Author,
            bookEntity.AvailableCopies
            );

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
        var books=await _context.
            Books.
            Where(book=>book.Title.Contains(searchTerm)|| book.Author.Contains(searchTerm)|| book.Isbn.Equals(searchTerm))
            .ToListAsync();
        
        return books.Select(book=>Book.CreateExisting(book.Id,
            book.Isbn,
            book.Title,
            new CategoryId(book.CategoryId),
            book.Author,
            book.AvailableCopies) 
            ).ToList();
    }
}