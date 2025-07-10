using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public interface IDataSeeder
{
    public static abstract void Configure(ModelBuilder modelBuilder);

}