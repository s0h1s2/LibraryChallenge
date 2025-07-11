using Core.Dto;

namespace Core.Entity;

public class User
{
    private User()
    {
    } // EF Core requires a parameterless constructor

    private User(string email, string password, Role role)
    {
        Email = email;
        PasswordHash = password;
        RoleId = role.Id;
        Role = role;
        Id = Guid.NewGuid();
    }

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Guid Id { get; private set; }
    public int RoleId { get; private set; }
    public Role Role { get; private set; }
    public virtual IList<BorrowedBook> BorrowedBooks { get; private set; } = new List<BorrowedBook>();

    public static User Create(string email, string password, Role role)
    {
        return new User(email, password, role);
    }


    public void AddBookToBorrowedBooks(Book book, DateTime dueDate)
    {
        if (book == null) throw new DomainException("Book cannot be null");
        BorrowedBooks.Add(new BorrowedBook(book, dueDate, this));
    }

    public void MarkBookAsReturned(Book book)
    {
        if (book == null) throw new DomainException("Book cannot be null");
        var borrowedBook = BorrowedBooks.FirstOrDefault(bb => bb.BookId == book.Id);
        if (borrowedBook == null) throw new DomainException("This book is not borrowed by the user");
        borrowedBook.MarkAsReturned(DateTime.Now);
        BorrowedBooks.Remove(borrowedBook);
    }

    public bool HasPermission(string name)
    {
        return Role.Permissions.Any(p => p.Name == name);
    }

    public void AssignRole(Role role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role), "Role cannot be null");
        RoleId = role.Id;
    }

    public void UpdateInfo(UpdateUser updateUser)
    {
        Email = updateUser.Email ?? Email;
        PasswordHash = updateUser.Password ?? PasswordHash;
        Role = updateUser.Role ?? Role;
        RoleId = updateUser.Role?.Id ?? RoleId;
    }
}