namespace Core.Persistance;

public interface IBookRepository
{
    Task <Book?> AddBookAsync(Book book);
    Task<IList<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(Guid id); 
    Task<bool> DeleteBookAsync(Guid id);
    Task<IEnumerable<Book>> FilterBooksAsync(string searchTerm);
}