using Core.Dto;
using Core.Entity;

namespace Core.Persistance;

public interface IBookRepository
{
    Task<IList<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(Guid id); 
    Task<Book?> AddBookAsync(CreateBook book);
    Task<Book> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(Guid id);
    Task<IEnumerable<Book>> FilterBooksAsync(string searchTerm);
    Task UpdateBookAndBorrowedBooksByUserAsync(Book book, User user);
    
}