using Core.Dto;

namespace Core;
public class Book
{
    public Guid Id { get; private set; }
    public string Isbn { get; private set; }
    public string Title { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public string Author { get; private set; }
    
    public int AvailableCopies { get; private set; }
    public DateTime DueDate { get; private set; }
    private Book(string isbn, string title, CategoryId categoryId, string author, int availableCopies)
    {
        Id = Guid.NewGuid();
        Isbn = isbn;
        Title = title;
        CategoryId = categoryId;
        Author = author;
        AvailableCopies = availableCopies;
    }

    public static Book CreateBook(string isbn, string title, CategoryId category, string author,int availableCopies)
    {
        return new Book(isbn, title, category, author,availableCopies);
    }
    public static Book CreateExisting(Guid id,string isbn, string title, CategoryId category, string author,int availableCopies)
    {
        if (id == Guid.Empty) throw new DomainException("Id cannot be empty");
        var book=new Book(isbn, title, category, author,availableCopies)
        {
            Id = id,
        };
        return book;
    }
    public void Borrow(DateTime dueDate)
    {
        if (this.AvailableCopies==0) throw new DomainException("Can't borrow book when no copies are available");
        this.AvailableCopies--;
        this.DueDate = dueDate;
    }

    public void Return()
    {
        this.AvailableCopies++;
    }
    public Book UpdateDetail(UpdateBook updateBook)
    {
        return CreateExisting(
            Id,
            updateBook.Isbn?? Isbn,
            updateBook.Title??Title,
            CategoryId??new CategoryId(updateBook.CategoryId),
            updateBook.Author?? Author,
            updateBook.AvailableCopies
            );
    }
}