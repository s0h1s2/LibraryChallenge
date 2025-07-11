using Core;

using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class CategorySeeder : IDataSeeder
{
    public static readonly Guid ScienceId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid NonFictionId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid FictionId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    public static readonly Guid HistoryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    public static readonly Guid ProgrammingId = Guid.Parse("55555555-5555-5555-5555-555555555555");

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            Category.CreateExisting(new CategoryId(FictionId), name: "Science"),
            Category.CreateExisting(new CategoryId(NonFictionId), name: "Non-Fiction"),
            Category.CreateExisting(new CategoryId(HistoryId), name: "History"),
            Category.CreateExisting(new CategoryId(ProgrammingId), name: "Programming"),
            Category.CreateExisting(new CategoryId(ScienceId), name: "Science")
        );
    }
}