using Core.Entity;
using Core.Services;
using Core.UnitTests.Persistance;
using Core.ValueObjects;

namespace Core.UnitTests;

public class UserTest
{
    [Fact]
    public void TestUserBorrowBook_Book_MustBe_Borrowed()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );
        var user = User.Create("johnDoe", "password123");
        var returnDate = DateTime.Now.AddDays(14);
        user.BorrowBook(book, returnDate);
        Assert.Single(user.BorrowedBooks);
        Assert.Equal(user.BorrowedBooks.First().Book, book);
        Assert.Equal(user.BorrowedBooks.First().DueDate, returnDate);
    }
    [Fact]
    public void TestUserBorrowBook_But_Copy_Doesnt_Exist_Must_Throw_Domain_Exception()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            0
        );
        var user = User.Create("johnDoe", "password123");
        Assert.Throws<DomainException>(() => user.BorrowBook(book, DateTime.Now.AddDays(14)));
    }
    [Fact]
    public void TestUserBorrowBook_But_DueDate_Is_Past_Must_Throw_Domain_Exception()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );
        var user = User.Create("johnDoe", "password123");
        Assert.Throws<ArgumentException>(() => user.BorrowBook(book, DateTime.Now.AddDays(-1)));
    }
    [Fact]
    public void TestUserReturnBook_Book_MustBe_Returned()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );
        var user = User.Create("johnDoe", "password123");
        var dueDate = DateTime.Now.AddDays(14);
        user.BorrowBook(book, dueDate);
        Assert.Single(user.BorrowedBooks);
        user.ReturnBook(book);
        Assert.Empty(user.BorrowedBooks);
    }
    
}