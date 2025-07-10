using Core;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class BookSeeder : IDataSeeder
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var books = new List<Book>
        {
            Book.CreateExisting(
                id: Guid.Parse("b0000001-0000-0000-0000-000000000001"),
                isbn: "978-0-306-40615-7",
                title: "The Art of Computer Programming",
                author: "Donald Knuth",
                availableCopies: 1,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000002-0000-0000-0000-000000000002"),
                isbn: "978-0-201-03801-9",
                title: "Structure and Interpretation of Computer Programs",
                author: "Harold Abelson, Gerald Jay Sussman",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000003-0000-0000-0000-000000000003"),
                isbn: "978-0-596-52068-7",
                title: "Learning Python",
                author: "Mark Lutz",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000004-0000-0000-0000-000000000004"),
                isbn: "978-0-13-110362-7",
                title: "The C Programming Language",
                author: "Brian W. Kernighan, Dennis M. Ritchie",
                availableCopies: 4,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000005-0000-0000-0000-000000000005"),
                isbn: "978-0-262-03384-8",
                title: "Introduction to Algorithms",
                author: "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000006-0000-0000-0000-000000000006"),
                isbn: "978-0-321-35668-0",
                title: "Design Patterns: Elements of Reusable Object-Oriented Software",
                author: "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000007-0000-0000-0000-000000000007"),
                isbn: "978-0-596-00482-9",
                title: "Head First Design Patterns",
                author: "Eric Freeman, Bert Bates, Kathy Sierra, Elisabeth Robson",
                availableCopies: 5,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000008-0000-0000-0000-000000000008"),
                isbn: "978-1-491-94602-9",
                title: "Fluent Python",
                author: "Luciano Ramalho",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000009-0000-0000-0000-000000000009"),
                isbn: "978-0-201-83595-2",
                title: "Refactoring: Improving the Design of Existing Code",
                author: "Martin Fowler",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000a-0000-0000-0000-00000000000a"),
                isbn: "978-0-201-48567-2",
                title: "Clean Code: A Handbook of Agile Software Craftsmanship",
                author: "Robert C. Martin",
                availableCopies: 4,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000b-0000-0000-0000-00000000000b"),
                isbn: "978-0-13-235088-4",
                title: "Clean Architecture",
                author: "Robert C. Martin",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000c-0000-0000-0000-00000000000c"),
                isbn: "978-0-201-37955-7",
                title: "The Mythical Man-Month",
                author: "Frederick P. Brooks Jr.",
                availableCopies: 1,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000d-0000-0000-0000-00000000000d"),
                isbn: "978-0-262-03384-8",
                title: "Artificial Intelligence: A Modern Approach",
                author: "Stuart Russell, Peter Norvig",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000e-0000-0000-0000-00000000000e"),
                isbn: "978-0-13-142901-7",
                title: "Algorithms",
                author: "Robert Sedgewick, Kevin Wayne",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b000000f-0000-0000-0000-00000000000f"),
                isbn: "978-1-59327-584-6",
                title: "Automate the Boring Stuff with Python",
                author: "Al Sweigart",
                availableCopies: 5,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000010-0000-0000-0000-000000000010"),
                isbn: "978-0-262-53305-8",
                title: "Deep Learning",
                author: "Ian Goodfellow, Yoshua Bengio, Aaron Courville",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000011-0000-0000-0000-000000000011"),
                isbn: "978-1-4919-1889-0",
                title: "Data Science from Scratch",
                author: "Joel Grus",
                availableCopies: 4,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000012-0000-0000-0000-000000000012"),
                isbn: "978-0-13-708107-3",
                title: "Continuous Delivery",
                author: "Jez Humble, David Farley",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.NonFictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000013-0000-0000-0000-000000000013"),
                isbn: "978-0-13-475759-9",
                title: "The Pragmatic Programmer",
                author: "Andrew Hunt, David Thomas",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.ProgrammingId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000014-0000-0000-0000-000000000014"),
                isbn: "978-1-491-95035-8",
                title: "Python for Data Analysis",
                author: "Wes McKinney",
                availableCopies: 3,
                category: new CategoryId(CategorySeeder.ScienceId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000015-0000-0000-0000-000000000015"),
                isbn: "978-0-7432-7356-5",
                title: "The Da Vinci Code",
                author: "Dan Brown",
                availableCopies: 6,
                category: new CategoryId(CategorySeeder.FictionId)
            ),
            Book.CreateExisting(
                id: Guid.Parse("b0000016-0000-0000-0000-000000000016"),
                isbn: "978-0-679-64087-6",
                title: "Guns, Germs, and Steel",
                author: "Jared Diamond",
                availableCopies: 2,
                category: new CategoryId(CategorySeeder.HistoryId)
            ),
        };

        modelBuilder.Entity<Book>().HasData(books);
    }
}