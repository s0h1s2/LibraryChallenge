namespace Core.Entity;

public class User
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public Guid Id { get; private set; }
    public Role Role { get; private set; }
    public ICollection<BorrowedBook> BorrowedBooks { get; private set; } = new List<BorrowedBook>();
    private User(string username, string password)
    {
        Username = username;
        Password = password;
        Id = Guid.NewGuid();
    }
    public static User Create(string username, string password)
    {
        return new User(username,password);
    }


    public void BorrowBook(Book book, DateTime dueDate)
    {
        if (book == null) throw new DomainException("Book cannot be null");
        if (dueDate <= DateTime.Now) throw new ArgumentException("Return date must be in the future");
        book.Borrow();
        BorrowedBooks.Add(new BorrowedBook(book, dueDate));
    }

    public void ReturnBook(Book book)
    {
        book.Return();
        var borrowedBook = BorrowedBooks.FirstOrDefault(bb => bb.Book.Id == book.Id);
        BorrowedBooks.Remove(borrowedBook);
    }
}