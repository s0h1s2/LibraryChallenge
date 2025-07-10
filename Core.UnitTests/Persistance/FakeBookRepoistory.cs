using Core;
using Core.Dto;
using Core.Entity;
using Core.Persistance;

namespace Core.UnitTests.Persistance;

public class FakeBookRepository:IBookRepository
{
    public List<Book> Books { get; private set; } = new();
    public Task<Book?> AddBookAsync(CreateBook bookToAdd)
    {
        var book= bookToAdd.ToBook();
        Books.Add(book);
        return Task.FromResult<Book?>(book);
    }

    
    public Task<Book> UpdateBookAsync(Book book)
    {
        Books=Books.
            Select(b => book.Id == b.Id ? Book.CreateExisting(b.Id,b.Isbn,b.Title,b.CategoryId,b.Author,b.AvailableCopies): b)
            .ToList();
        return Task.FromResult(book);
    }


    public Task<IList<Book>> GetBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetBookByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Book>> FilterBooksAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }
}