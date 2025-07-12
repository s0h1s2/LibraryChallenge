using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class Book
{
    private Book(string isbn, string title, CategoryId categoryId, string author, int availableCopies, int totalCopies)
    {
        Id = Guid.NewGuid();
        Isbn = isbn;
        Title = title;
        CategoryId = categoryId;
        Author = author;
        AvailableCopies = availableCopies;
        TotalCopies = totalCopies;
    }

    public Guid Id { get; private set; }
    public string Isbn { get; private set; }
    public string Title { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Author { get; private set; }

    public int AvailableCopies { get; private set; }
    public int TotalCopies { get; private set; }

    public static Book CreateBook(string isbn, string title, CategoryId category, string author, int availableCopies,
        int totalCopies)
    {
        return new Book(isbn, title, category, author, availableCopies, totalCopies);
    }

    public static Book CreateExisting(Guid id, string isbn, string title, CategoryId category, string author,
        int availableCopies, int totalCopies)
    {
        if (id == Guid.Empty) throw new DomainException("Id cannot be empty");
        var book = new Book(isbn, title, category, author, availableCopies, totalCopies)
        {
            Id = id,
        };
        return book;
    }

    public void Borrow()
    {
        if (AvailableCopies == 0) throw new DomainException("Can't borrow book when no copies are available");
        AvailableCopies--;
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new DomainException("Available Copies must be less or equal to total copies");

        AvailableCopies++;
    }

    public void UpdateDetail(UpdateBook updateBook)
    {
        Isbn = updateBook.Isbn ?? Isbn;
        Title = updateBook.Title ?? Title;
        if (updateBook.CategoryId.HasValue)
        {
            CategoryId = new CategoryId(updateBook.CategoryId.GetValueOrDefault());
        }

        Author = updateBook.Author ?? Author;
        AvailableCopies = updateBook.AvailableCopies ?? AvailableCopies;
        TotalCopies = updateBook.TotalCopies ?? TotalCopies;
    }
}