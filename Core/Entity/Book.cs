using Core.Dto;

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
    public string Isbn { get; }
    public string Title { get; }
    public CategoryId CategoryId { get; }
    public Category Category { get; private set; }
    public string Author { get; }

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

    public Book UpdateDetail(UpdateBook updateBook)
    {
        return CreateExisting(
            Id,
            updateBook.Isbn ?? Isbn,
            updateBook.Title ?? Title,
            CategoryId,
            updateBook.Author ?? Author,
            updateBook.AvailableCopies ?? AvailableCopies,
            totalCopies: updateBook.TotalCopies ?? TotalCopies
        );
    }
}