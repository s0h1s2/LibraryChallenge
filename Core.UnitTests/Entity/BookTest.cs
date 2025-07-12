using Core.Dto;
using Core.Entity;
using Core.UnitTests.Persistance;
using Core.ValueObjects;

namespace Core.UnitTests.Entity;

public class BookTest : IDisposable
{
    private readonly string _defaultAuthor;
    private readonly CategoryId _defaultCategoryId;
    private readonly string _defaultIsbn;
    private readonly string _defaultTitle;
    private readonly int _defaultTotalCopies;

    public BookTest()
    {
        _defaultIsbn = "978-0-306-40615-7";
        _defaultTitle = "The Art of Computer Programming";
        _defaultCategoryId = new CategoryId(Guid.NewGuid());
        _defaultAuthor = "Donald Knuth";
        _defaultTotalCopies = 10;
    }

    public void Dispose()
    {
    }

    private Book CreateBook(int availableCopies, int? totalCopies = null)
    {
        return Book.CreateBook(
            _defaultIsbn,
            _defaultTitle,
            _defaultCategoryId,
            _defaultAuthor,
            availableCopies,
            totalCopies ?? _defaultTotalCopies
        );
    }

    [Fact]
    public void TestBorrowBookFromLibrary_AvailableCopies_Is_Zero_Must_Throw_Domain_Exception()
    {
        var book = CreateBook(availableCopies: 0);
        Assert.Throws<DomainException>(() => book.Borrow());
    }

    [Fact]
    public void TestUpdateBookInLibrary_Book_MustBe_Updated()
    {
        var bookRepo = new FakeBookRepository();
        var bookToUpdate = new CreateBook(
                "12354",
                _defaultTitle,
                Guid.NewGuid(),
                _defaultAuthor,
                1,
                _defaultTotalCopies
            )
            .ToBook();
        bookRepo.Books.Add(bookToUpdate);
        var book = bookRepo.Books.First();
        var updatedBook = book.UpdateDetail(new UpdateBook("The Art Of Computer Programming",
            "12354",
            book.CategoryId,
            _defaultAuthor,
            2,
            0));
        bookRepo.UpdateBookAsync(updatedBook);
        Assert.NotEqual(book, bookRepo.Books.First());
    }

    [Fact]
    public void TestBorrowBookFromLibrary_AvailableCopies_Must_Decrease_AfterBorrowing()
    {
        var book = CreateBook(availableCopies: 2);
        book.Borrow();
        Assert.Equal(1, book.AvailableCopies);
    }

    [Fact]
    public void TestReturnBook_After_Borrow_AvailableCopies_Must_Increase()
    {
        var book = CreateBook(availableCopies: 1);

        book.Borrow();
        Assert.Equal(0, book.AvailableCopies);
        book.Return();
        Assert.Equal(1, book.AvailableCopies);
    }

    [Fact]
    public void TestReturnBook_Available_Book_Shouldnot_Be_More_Than_Total_Copy_Should_Throw_Domain_Exception()
    {
        var book = CreateBook(availableCopies: 1, totalCopies: 1);
        Assert.Throws<DomainException>(() => book.Return());
    }
}