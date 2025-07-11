using Core.Dto;
using Core.Entity;
using Core.Persistance;

namespace Core.Services;

public class BookDomainService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public BookDomainService(IBookRepository bookRepository, IUserRepository userRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<Book?> AddBookAsync(CreateBook book)
    {
        return await _bookRepository.AddBookAsync(book);
    }

    public async Task BorrowBookAsync(BorrowBook borrowBook, Guid userId)
    {
        var book = await _bookRepository.GetBookByIdAsync(borrowBook.BookId);
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (book is null || user is null)
        {
            throw new KeyNotFoundException();
        }
        book.Borrow(user, borrowBook.DueDate);
        await _bookRepository.UpdateBookAndBorrowedBooksByUserAsync(book, user);
    }

    public async Task ReturnBookAsync(Guid bookId, Guid userId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);
        var user = await _userRepository.GetUserWithBorrowedBooksAsync(userId, bookId);
        if (book is null || user is null)
        {
            throw new KeyNotFoundException();
        }
        book.ReturnBy(user);
        await _bookRepository.UpdateBookAndBorrowedBooksByUserAsync(book, user);
    }

}