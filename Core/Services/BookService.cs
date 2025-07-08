using Core.Dto;
using Core.Persistance;

namespace Core.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> AddBookAsync(CreateBook book)
    {
        return await _bookRepository.AddBookAsync(book);
    }
}