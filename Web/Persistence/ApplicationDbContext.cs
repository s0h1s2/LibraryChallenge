using System.Reflection;
using Core;
using Core.Entity;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistence;

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
        optionsBuilder.UseSeeding(((context, b) =>
        {
            var adminRole = Core.Entity.Role.Create(RoleType.Admin);
            var liberianRole = Core.Entity.Role.Create(RoleType.Liberian);
            var memberRole = Core.Entity.Role.Create(RoleType.Member);
            var permissions = Enum.GetValues<PermissionType>()
                .Select((p) => Core.Entity.Permission.Create(p))
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
        }));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}