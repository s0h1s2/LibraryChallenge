using Core;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options)
{
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<PermissionEntity> Permission { get; set; }
    public DbSet<UserRoleEntity> UserRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
        optionsBuilder.UseSeeding(((context, b) =>
        {
            var categories = new List<CategoryEntity>()
            {
                new CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Science",
                },
                new CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Non-Fiction",
                },
                new CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fiction",
                },
                new CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "History",
                },
                new CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Programming",
                },
            };

            context.Set<CategoryEntity>().AddRange(categories);
// Create random instance for category assignment
            var random = new Random();
            context.Set<BookEntity>().AddRange(new List<BookEntity>()
            {
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-306-40615-7",
                    Title = "The Art of Computer Programming",
                    Author = "Donald Knuth",
                    AvailableCopies = 1,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-201-63361-2",
                    Title = "Design Patterns",
                    Author = "Gang of Four",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-262-03384-8",
                    Title = "Introduction to Algorithms",
                    Author = "Thomas H. Cormen",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-1-59327-584-6",
                    Title = "Automate the Boring Stuff with Python",
                    Author = "Al Sweigart",
                    AvailableCopies = 4,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-321-35668-2",
                    Title = "Effective Java",
                    Author = "Joshua Bloch",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-13-110362-8",
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt",
                    AvailableCopies = 5,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-06-112008-4",
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-14-243723-0",
                    Title = "1984",
                    Author = "George Orwell",
                    AvailableCopies = 6,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-14-243722-3",
                    Title = "Animal Farm",
                    Author = "George Orwell",
                    AvailableCopies = 4,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-452-28423-4",
                    Title = "Brave New World",
                    Author = "Aldous Huxley",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-375-41398-8",
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-486-27557-8",
                    Title = "A Brief History of Time",
                    Author = "Stephen Hawking",
                    AvailableCopies = 4,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-14-143951-8",
                    Title = "The Origin of Species",
                    Author = "Charles Darwin",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-670-81693-1",
                    Title = "The Selfish Gene",
                    Author = "Richard Dawkins",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-1-4000-3244-6",
                    Title = "Sapiens",
                    Author = "Yuval Noah Harari",
                    AvailableCopies = 5,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-14-303989-3",
                    Title = "The Guns of August",
                    Author = "Barbara Tuchman",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-679-64115-3",
                    Title = "A People's History of the United States",
                    Author = "Howard Zinn",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-14-028329-3",
                    Title = "The Diary of a Young Girl",
                    Author = "Anne Frank",
                    AvailableCopies = 4,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-679-76389-4",
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    AvailableCopies = 3,
                    CategoryId = categories[random.Next(categories.Count)].Id
                },
                new BookEntity()
                {
                    Id = Guid.NewGuid(),
                    Isbn = "978-0-553-21311-4",
                    Title = "Dune",
                    Author = "Frank Herbert",
                    AvailableCopies = 2,
                    CategoryId = categories[random.Next(categories.Count)].Id
                }
            });
            context.Add(new RoleEntity()
            {
                Name = nameof(RoleType.Admin),
                Permissions = Enum.GetValues<AttributeType>().Select(perm=>new PermissionEntity
                {
                    Name = perm.ToString()
                }).ToList(),
            });
            context.SaveChanges();
        }));
    }
}