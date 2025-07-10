namespace Core.Entity;

public class BorrowedBook
{
    public Guid Id { get; private set; } 
    
    public Guid BookId { get; private set; }
    public Book Book { get; private set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }
    public BorrowedBook(Book book, DateTime dueDate,User user)
    {
        Id=Guid.NewGuid();
        Book = book;
        DueDate = dueDate;
    }
    public void MarkAsReturned(DateTime returnDate)
    {
        if (returnDate < DueDate) throw new DomainException("Return date cannot be before due date");
        ReturnDate = returnDate;
    }
}