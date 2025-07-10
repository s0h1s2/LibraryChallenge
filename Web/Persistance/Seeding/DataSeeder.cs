using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class DataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        CategorySeeder.Configure(modelBuilder);
        BookSeeder.Configure(modelBuilder);
        RoleSeeder.Configure(modelBuilder);
        PermissionSeeder.Configure(modelBuilder);
        UserSeeder.Configure(modelBuilder);
    }
}