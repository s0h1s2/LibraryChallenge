using Core.Entity;
using Core.Services;
using Core.ValueObjects;

namespace Core.UnitTests.Services;

public class LibraryServiceTest : IDisposable
{
    private readonly Book _book;
    private readonly DateTime _dueDate;
    private readonly User _user;

    public LibraryServiceTest()
    {
        _book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1,
            10
        );
        _user = User.Create("shkar@mail.com", "password", Role.Create(RoleType.Admin));
        _dueDate = new DateTime(2025, 07, 01);
    }

    public void Dispose()
    {
    }

    [Fact]
    public void TestUserBorrowBook_Book_Must_Be_Borrowed()
    {
        var lib = new LibraryService();
        lib.BorrowBook(_book, _user, _dueDate);
        Assert.Single(_user.BorrowedBooks);
        Assert.Equal(0, _book.AvailableCopies);
    }

    [Fact]
    public void TestUserReturnBook_Book_Must_Be_Returned()
    {
        var lib = new LibraryService();
        lib.BorrowBook(_book, _user, _dueDate);
        Assert.Single(_user.BorrowedBooks);
        lib.ReturnBook(_book, _user);
        Assert.Contains(_user.BorrowedBooks, br => br.IsReturned);
        Assert.Equal(1, _book.AvailableCopies);
    }
}