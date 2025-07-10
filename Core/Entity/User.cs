namespace Core.Entity;

public class User
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Guid Id { get; private set; }
    public int RoleId { get; private set; }
    public Role Role { get; private set; }
    public ICollection<BorrowedBook> BorrowedBooks { get; private set; } = new List<BorrowedBook>();
    private User() { } // EF Core requires a parameterless constructor
    private User(string email, string password,int roleId)
    {
        Email= email;
        PasswordHash= password;
        RoleId = roleId;
        Id = Guid.NewGuid();
    }
    public static User Create(string email, string password,int roleId)
    {
        return new User(email,password,roleId);
    }
    public void AddBookToBorrowedBooks(Book book, DateTime dueDate)
    {
        if (book == null) throw new DomainException("Book cannot be null");
        BorrowedBooks.Add(new BorrowedBook(book, dueDate, this));
    }

    public void MarkBookAsReturned(Book book)
    {
        if (book == null) throw new DomainException("Book cannot be null");
        var borrowedBook = BorrowedBooks.FirstOrDefault(bb => bb.BookId == book.Id && !bb.IsReturned);
        if (borrowedBook == null) throw new DomainException("This book is not borrowed by the user");
        borrowedBook.MarkAsReturned(DateTime.Now);
        BorrowedBooks.Remove(borrowedBook);
    }
}