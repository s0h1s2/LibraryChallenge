namespace Core.Entity;

public class BorrowedBook
{
    public Guid Id { get; private set; } 
    public Book Book { get; private set; }
    public DateTime DueDate { get; private set; }
    public BorrowedBook(Book book, DateTime dueDate)
    {
        Id=Guid.NewGuid();
        Book = book;
        DueDate = dueDate;
    }
    
}