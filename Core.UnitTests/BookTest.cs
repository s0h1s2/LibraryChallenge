using Core.Dto;
using Core.Entity;
using Core.Services;
using Core.UnitTests.Persistance;

namespace Core.UnitTests;

public class BookTest : IDisposable
{
    public User User { get; set; }

    public BookTest()
    {
        User = User.Create("shkar",
            "password123",
            1
        );
    }
    public void Dispose()
    {
    }
    [Fact]
    public async Task TestAddBookToLibrary_Book_MustBe_Added()
    {
        var bookToAdd = new CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            Guid.NewGuid(),
            "Donald Knuth",
            1
            );
        var bookRepo = new FakeBookRepository();
        var userRepo = new FakeUserRepository();
        var bookService = new BookDomainService(bookRepo, userRepo);
        var book = await bookService.AddBookAsync(bookToAdd);
        Assert.Single(bookRepo.Books);
        Assert.Equivalent(book, bookRepo.Books.First());
    }
    [Fact]
    public void TestBorrowBookFromLibrary_AvailableCopies_Is_Zero_Must_Throw_Domain_Exception()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            0
        );
        Assert.Throws<DomainException>(() => book.Borrow(User, DateTime.Now));
    }
    [Fact]
    public void TestUpdateBookInLibrary_Book_MustBe_Updated()
    {
        var bookRepo = new FakeBookRepository();
        var bookToUpdate = new CreateBook(
                "12354",
            "The Art of Computer Programming",
            Guid.NewGuid(),
            "Donald Knuth",
            1)
            .ToBook();
        bookRepo.Books.Add(bookToUpdate);
        var book = bookRepo.Books.First();
        var updatedBook = book.UpdateDetail(new UpdateBook("The Art Of Computer Programming",
            "12354",
            book.CategoryId,
            "Donald Knuth",
            2));
        bookRepo.UpdateBookAsync(updatedBook);
        Assert.NotEqual(book, bookRepo.Books.First());
    }
    [Fact]
    public void TestBorrowBookFromLibrary_AvailableCopies_Must_Decrease_AfterBorrowing()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            2
        );
        var returnDate = DateTime.Now.AddDays(14);
        book.Borrow(User, returnDate);
        Assert.Equal(1, book.AvailableCopies);

    }
    [Fact]
    public void TestReturnBook_After_Borrow_AvailableCopies_Must_Increase()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );
        var returnDate = DateTime.Now.AddDays(14);
        book.Borrow(User, returnDate);
        Assert.Equal(0, book.AvailableCopies);
        book.ReturnBy(User);
        Assert.Equal(1, book.AvailableCopies);
    }
    [Fact]
    public void TestBorrowBook_By_User_Must_Be_Added_To_BorrowedBooks()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );

        var returnDate = DateTime.Now.AddDays(14);
        book.Borrow(User, returnDate);
        Assert.Single(User.BorrowedBooks);
        Assert.Equal(book, User.BorrowedBooks.First().Book);
        Assert.Equal(returnDate, User.BorrowedBooks.First().DueDate);
    }
    [Fact]
    public void TestUserReturnBorrowedBook_Book_Must_Be_Returned()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
        );

        var returnDate = DateTime.Now.AddDays(14);
        book.Borrow(User, returnDate);
        Assert.Single(User.BorrowedBooks);
        book.ReturnBy(User);
        Assert.Empty(User.BorrowedBooks);
    }


}