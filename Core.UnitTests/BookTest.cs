using Core.UnitTests.Persistance;
using Core;
using Core.Services;

namespace Core.UnitTests;

public class BookTest
{
    [Fact]
    public async Task TestAddBookToLibrary_Book_MustBe_Added()
    {
        var book = Book.CreateBook(
            "978-0-306-40615-7",
            "The Art of Computer Programming",
            new CategoryId(Guid.NewGuid()),
            "Donald Knuth",
            1
            );
        var bookRepo = new FakeBookRepository();
        var bookService = new BookService(bookRepo);
        await bookService.AddBookAsync(book);
        Assert.NotEmpty(bookRepo.Books);
        Assert.Same(book,bookRepo.Books.First());
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
        Assert.Throws<DomainException>(()=>book.Borrow(DateTime.Now));
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
        book.Borrow(DateTime.Now);
        Assert.Equal(1,book.AvailableCopies);
        
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
        book.Borrow(DateTime.Now);
        Assert.Equal(0,book.AvailableCopies);
        book.Return();
        Assert.Equal(1,book.AvailableCopies);
    }
}