using Core.Entity;

namespace Core.Services;

public class LibraryService
{
    public void BorrowBook(Book book, User user, DateTime dueDate)
    {
        book.Borrow();
        user.AddBookToBorrowedBooks(book, dueDate);
    }

    public void ReturnBook(Book book, User user)
    {
        book.Return();
        user.MarkBookAsReturned(book);
    }
}