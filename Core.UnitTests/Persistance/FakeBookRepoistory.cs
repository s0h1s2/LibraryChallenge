using Core;
using Core.Persistance;

namespace Core.UnitTests.Persistance;

public class FakeBookRepository:IBookRepository
{
    public List<Book> Books { get; } = new();
    public Task<Book?> AddBookAsync(Book book)
    {
        Books.Add(book);
        return Task.FromResult<Book?>(book);
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