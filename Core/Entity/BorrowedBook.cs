namespace Core.Entity;

public class BorrowedBook
{
    private BorrowedBook()
    {
    }

    public BorrowedBook(Book book, DateTime dueDate, User user)
    {
        Id = Guid.NewGuid();
        BookId = book.Id;
        UserId = user.Id;
        Book = book;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public Guid Id { get; private set; }

    public Guid BookId { get; private set; }
    public Book Book { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned => ReturnDate != null;

    public void MarkAsReturned(DateTime returnDate)
    {
        ReturnDate = returnDate;
    }

    public void ExtendDue(DateTime newDueDate)
    {
        if (newDueDate < DueDate) throw new DomainException("New due date must be greater than current due date");
        DueDate = newDueDate;
    }
}