using Core;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Persistance.Seeding;

namespace Web.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options)
{
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<PermissionEntity> Permission { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        DataSeeder.SeedData(modelBuilder);
    }
}