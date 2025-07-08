using Domain;
using Domain.Persistance;

namespace UnitTests.Persistance;

public class FakeBookRepository:IBookRepository
{
    public List<Book> Books { get; } = new();
    public Task<Book?> AddBookAsync(Book book)
    {
        Books.Add(book);
        return Task.FromResult<Book?>(book);
    }
}