using Core.Dto;
using Core.Entity;
using Core.Persistance;

namespace Core.Services;

public class BookDomainService
{
    private readonly IBookRepository _bookRepository;
    
    public BookDomainService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> AddBookAsync(CreateBook book)
    {
        return await _bookRepository.AddBookAsync(book);
    }

    public async Task BorrowBookAsync(BorrowBook borrowBook)
    {
        var book=await _bookRepository.GetBookByIdAsync(borrowBook.BookId);
        if (book is null)
        {
            throw new KeyNotFoundException();
        }
        book.Borrow();
        await _bookRepository.UpdateBookAsync(book);   
    }
}