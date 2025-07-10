using System.Reflection;
using Core;
using Core.Entity;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Persistance.Seeding;

namespace Web.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<User?> User { get; set; }
    
    public DbSet<Role> Role { get; set; }
    public DbSet<RolePermission> Permission { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        DataSeeder.SeedData(modelBuilder);
    }
}