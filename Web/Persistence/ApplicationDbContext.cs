using System.Reflection;
using Core.Entity;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Util.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<User> User { get; set; }

    public DbSet<Role> Role { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
        optionsBuilder.UseSeeding((context, b) =>
        {
            var adminRole = Core.Entity.Role.Create(RoleType.Admin);
            var liberianRole = Core.Entity.Role.Create(RoleType.Liberian);
            var memberRole = Core.Entity.Role.Create(RoleType.Member);
            var permissions = Enum.GetValues<PermissionType>()
                .Select(p => Core.Entity.Permission.Create(p))
                .ToList();
            adminRole.AssignPermissions(permissions);
            context.Set<Permission>().AddRange(permissions);
            context.Set<Role>().AddRange(adminRole, liberianRole, memberRole);

            var hashPassword = new PasswordHasher<object?>();
            context.Set<User>().AddRange(
                Core.Entity.User.Create("user@mail.com", hashPassword.HashPassword(null, "password"), adminRole),
                Core.Entity.User.Create("broosk@mail.com", hashPassword.HashPassword(null, "password"), adminRole),
                Core.Entity.User.Create("sazan@mail.com", hashPassword.HashPassword(null, "password"), adminRole),
                Core.Entity.User.Create("handren@mail.com", hashPassword.HashPassword(null, "password"), adminRole),
                Core.Entity.User.Create("member@mail.com", hashPassword.HashPassword(null, "password"), memberRole),
                Core.Entity.User.Create("liberian@mail.com", hashPassword.HashPassword(null, "password"), liberianRole)
            );
            context.SaveChanges();
            var programmingCategory = Core.Entity.Category.Create("Programming");
            var scienceCategory = Core.Entity.Category.Create("Science");
            var fictionCategory = Core.Entity.Category.Create("Fiction");
            context.Set<Category>().AddRange(programmingCategory, scienceCategory, fictionCategory);
            context.SaveChanges();
            context.Set<Book>().AddRange(
                // Programming Category Books (7 books)
                Book.CreateBook(
                    "12345",
                    "The Art of Computer Programming",
                    programmingCategory.Id,
                    "Donald Knuth",
                    10, 10
                ),
                Book.CreateBook(
                    "67890",
                    "Clean Code",
                    programmingCategory.Id,
                    "Robert C. Martin",
                    8, 15
                ),
                Book.CreateBook(
                    "11111",
                    "Design Patterns",
                    programmingCategory.Id,
                    "Gang of Four",
                    12, 8
                ),
                Book.CreateBook(
                    "22222",
                    "JavaScript: The Good Parts",
                    programmingCategory.Id,
                    "Douglas Crockford",
                    6, 20
                ),
                Book.CreateBook(
                    "33333",
                    "Refactoring",
                    programmingCategory.Id,
                    "Martin Fowler",
                    9, 12
                ),
                Book.CreateBook(
                    "44444",
                    "The Pragmatic Programmer",
                    programmingCategory.Id,
                    "Andrew Hunt",
                    7, 18
                ),
                Book.CreateBook(
                    "55555",
                    "Code Complete",
                    programmingCategory.Id,
                    "Steve McConnell",
                    11, 9
                ),

                // Science Category Books (7 books)
                Book.CreateBook(
                    "66666",
                    "A Brief History of Time",
                    scienceCategory.Id,
                    "Stephen Hawking",
                    5, 25
                ),
                Book.CreateBook(
                    "77777",
                    "The Origin of Species",
                    scienceCategory.Id,
                    "Charles Darwin",
                    8, 14
                ),
                Book.CreateBook(
                    "88888",
                    "Cosmos",
                    scienceCategory.Id,
                    "Carl Sagan",
                    6, 22
                ),
                Book.CreateBook(
                    "99999",
                    "The Double Helix",
                    scienceCategory.Id,
                    "James Watson",
                    4, 28
                ),
                Book.CreateBook(
                    "10101",
                    "Silent Spring",
                    scienceCategory.Id,
                    "Rachel Carson",
                    7, 16
                ),
                Book.CreateBook(
                    "20202",
                    "The Selfish Gene",
                    scienceCategory.Id,
                    "Richard Dawkins",
                    9, 13
                ),
                Book.CreateBook(
                    "30303",
                    "Sapiens",
                    scienceCategory.Id,
                    "Yuval Noah Harari",
                    10, 11
                ),

                // Fiction Category Books (6 books)
                Book.CreateBook(
                    "40404",
                    "1984",
                    fictionCategory.Id,
                    "George Orwell",
                    5, 30
                ),
                Book.CreateBook(
                    "50505",
                    "To Kill a Mockingbird",
                    fictionCategory.Id,
                    "Harper Lee",
                    6, 26
                ),
                Book.CreateBook(
                    "60606",
                    "The Great Gatsby",
                    fictionCategory.Id,
                    "F. Scott Fitzgerald",
                    4, 35
                ),
                Book.CreateBook(
                    "70707",
                    "Pride and Prejudice",
                    fictionCategory.Id,
                    "Jane Austen",
                    7, 19
                ),
                Book.CreateBook(
                    "80808",
                    "The Catcher in the Rye",
                    fictionCategory.Id,
                    "J.D. Salinger",
                    5, 24
                ),
                Book.CreateBook(
                    "90909",
                    "Lord of the Flies",
                    fictionCategory.Id,
                    "William Golding",
                    6, 21
                )
            );
            context.SaveChanges();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}