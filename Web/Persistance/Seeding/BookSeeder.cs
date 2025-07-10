using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class BookSeeder:IDataSeeder
{
    public static void Configure(ModelBuilder modelBuilder)
    { var books = new List<BookEntity>
        {
            new()
            {
                Id = Guid.Parse("b0000001-0000-0000-0000-000000000001"),
                Isbn = "978-0-306-40615-7",
                Title = "The Art of Computer Programming",
                Author = "Donald Knuth",
                AvailableCopies = 1,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000002-0000-0000-0000-000000000002"),
                Isbn = "978-0-201-63361-2",
                Title = "Design Patterns",
                Author = "Gang of Four",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000003-0000-0000-0000-000000000003"),
                Isbn = "978-0-262-03384-8",
                Title = "Introduction to Algorithms",
                Author = "Thomas H. Cormen",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000004-0000-0000-0000-000000000004"),
                Isbn = "978-1-59327-584-6",
                Title = "Automate the Boring Stuff with Python",
                Author = "Al Sweigart",
                AvailableCopies = 4,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000005-0000-0000-0000-000000000005"),
                Isbn = "978-0-321-35668-2",
                Title = "Effective Java",
                Author = "Joshua Bloch",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000006-0000-0000-0000-000000000006"),
                Isbn = "978-0-13-110362-8",
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                AvailableCopies = 5,
                CategoryId = CategorySeeder.ProgrammingId
            },
            new()
            {
                Id = Guid.Parse("b0000007-0000-0000-0000-000000000007"),
                Isbn = "978-0-06-112008-4",
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000008-0000-0000-0000-000000000008"),
                Isbn = "978-0-14-243723-0",
                Title = "1984",
                Author = "George Orwell",
                AvailableCopies = 6,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000009-0000-0000-0000-000000000009"),
                Isbn = "978-0-14-243722-3",
                Title = "Animal Farm",
                Author = "George Orwell",
                AvailableCopies = 4,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000010-0000-0000-0000-000000000010"),
                Isbn = "978-0-452-28423-4",
                Title = "Brave New World",
                Author = "Aldous Huxley",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000011-0000-0000-0000-000000000011"),
                Isbn = "978-0-375-41398-8",
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000012-0000-0000-0000-000000000012"),
                Isbn = "978-0-486-27557-8",
                Title = "A Brief History of Time",
                Author = "Stephen Hawking",
                AvailableCopies = 4,
                CategoryId = CategorySeeder.ScienceId
            },
            new()
            {
                Id = Guid.Parse("b0000013-0000-0000-0000-000000000013"),
                Isbn = "978-0-14-143951-8",
                Title = "The Origin of Species",
                Author = "Charles Darwin",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.ScienceId
            },
            new()
            {
                Id = Guid.Parse("b0000014-0000-0000-0000-000000000014"),
                Isbn = "978-0-670-81693-1",
                Title = "The Selfish Gene",
                Author = "Richard Dawkins",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.ScienceId
            },
            new()
            {
                Id = Guid.Parse("b0000015-0000-0000-0000-000000000015"),
                Isbn = "978-1-4000-3244-6",
                Title = "Sapiens",
                Author = "Yuval Noah Harari",
                AvailableCopies = 5,
                CategoryId = CategorySeeder.HistoryId
            },
            new()
            {
                Id = Guid.Parse("b0000016-0000-0000-0000-000000000016"),
                Isbn = "978-0-14-303989-3",
                Title = "The Guns of August",
                Author = "Barbara Tuchman",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.HistoryId
            },
            new()
            {
                Id = Guid.Parse("b0000017-0000-0000-0000-000000000017"),
                Isbn = "978-0-679-64115-3",
                Title = "A People's History of the United States",
                Author = "Howard Zinn",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.HistoryId
            },
            new()
            {
                Id = Guid.Parse("b0000018-0000-0000-0000-000000000018"),
                Isbn = "978-0-14-028329-3",
                Title = "The Diary of a Young Girl",
                Author = "Anne Frank",
                AvailableCopies = 4,
                CategoryId = CategorySeeder.HistoryId
            },
            new()
            {
                Id = Guid.Parse("b0000019-0000-0000-0000-000000000019"),
                Isbn = "978-0-679-76389-4",
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                AvailableCopies = 3,
                CategoryId = CategorySeeder.FictionId
            },
            new()
            {
                Id = Guid.Parse("b0000020-0000-0000-0000-000000000020"),
                Isbn = "978-0-553-21311-4",
                Title = "Dune",
                Author = "Frank Herbert",
                AvailableCopies = 2,
                CategoryId = CategorySeeder.FictionId
            }
        };

        modelBuilder.Entity<BookEntity>().HasData(books);
        
    }
}