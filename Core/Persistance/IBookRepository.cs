namespace Core.Persistance;

public interface IBookRepository
{
    Task <Book?> AddBookAsync(Book book);
    Task<IList<Book>> GetBooksAsync();
}