using Domain.Persistance;

namespace Domain.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> AddBookAsync(Book book)
    {
        return await _bookRepository.AddBookAsync(book);
    }
}