using Core.Entity;
using Core.Services;
using Core.ValueObjects;

namespace Core.UnitTests.Entity;

public class BorrowedBookTest : IDisposable
{
    private readonly LibraryService _libService;
    private readonly User _user;
    private Book _book;

    public BorrowedBookTest()
    {
        _libService = new LibraryService();
        _user = User.Create("user@mail.com", "password", Role.Create(RoleType.Admin));
        _book = Book.CreateBook("111111111111", "aa", new CategoryId(Guid.NewGuid()), "shkar", 4, 5);
    }

    public void Dispose()
    {
    }

    [Fact]
    public void ExtendDueDate_WhenUserHasBorrowedBook_ShouldExtendDueDate()
    {
        var dueDate = new DateTime(2025, 07, 15);
        var newDueDate = new DateTime(2025, 07, 20);
        _user.AddBookToBorrowedBooks(_book, dueDate);
        var borrowedBook = _user.BorrowedBooks.First();
        borrowedBook.ExtendDue(newDueDate);
        Assert.Equal(borrowedBook.DueDate, newDueDate);
    }

    [Fact]
    public void ExtendDueDate_Should_Fail_When_Date_In_Past_With_CurrentDueDate()
    {
        var dueDate = new DateTime(2025, 07, 15);
        var newDueDate = new DateTime(2025, 07, 10);
        _user.AddBookToBorrowedBooks(_book, dueDate);
        Assert.Single(_user.BorrowedBooks);
        var borrowedBook = _user.BorrowedBooks.First();
        Assert.Throws<DomainException>(() => borrowedBook.ExtendDue(newDueDate));
    }
}