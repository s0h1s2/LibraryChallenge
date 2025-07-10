using Core.UnitTests.Persistance;
using Core.Dto;
using Core.Entity;
using Core.Services;

namespace Core.UnitTests;

public class BookTest
{
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
        var bookService = new BookDomainService(bookRepo,userRepo);
        var book=await bookService.AddBookAsync(bookToAdd);
        Assert.Single(bookRepo.Books);
        Assert.Equivalent(book,bookRepo.Books.First());
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
        Assert.Throws<DomainException>(()=>book.Borrow());
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
        var book=bookRepo.Books.First();
        var updatedBook=book.UpdateDetail(new UpdateBook("The Art Of Computer Programming",
            "12354",
            book.CategoryId,
            "Donald Knuth",
            2));
        bookRepo.UpdateBookAsync(updatedBook);
        Assert.NotEqual(book,bookRepo.Books.First());
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
        book.Borrow();
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
        book.Borrow();
        Assert.Equal(0,book.AvailableCopies);
        book.Return();
        Assert.Equal(1,book.AvailableCopies);
    }
}