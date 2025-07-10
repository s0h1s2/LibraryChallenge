using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class UserSeeder: IDataSeeder
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<object?>();
        var users = new List<UserEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Email = "shkar@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.AdminRoleId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "broosk@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.AdminRoleId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "handren@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.AdminRoleId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "sazan@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.AdminRoleId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "john@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.MemberRoleId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "liberian@mail.com",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
                RoleId = RoleSeeder.LibrarianRoleId
            },

        };

        modelBuilder.Entity<UserEntity>().HasData(users);
    }
}