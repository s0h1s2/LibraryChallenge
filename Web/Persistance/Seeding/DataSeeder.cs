using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class DataSeeder
{
    
    public static void SeedData(ModelBuilder modelBuilder)
    {
        var seederType = typeof(IDataSeeder);
        var seeders = typeof(DataSeeder).Assembly
            .GetTypes()
            .Where(t => seederType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var seeder in seeders)
        {
            var method = seeder.GetMethod("Configure", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            method?.Invoke(null, new object[] { modelBuilder });
        }
    }
}