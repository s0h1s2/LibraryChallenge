using Core;
using Core.Persistance;

namespace Web.Persistance;

public class BookRepository:IBookRepository
{
    public BookRepository()
    {
        
    }
    public Task<Book?> AddBookAsync(Book book)
    {
        return null;
    }
}