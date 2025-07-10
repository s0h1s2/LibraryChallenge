using Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class UserSeeder: IDataSeeder
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<object?>();
        var users = new List<User>
        {
            User.Create(
                email : "shkar@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.AdminRoleId
                ),
            User.Create(
                email : "broosk@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.AdminRoleId
            ),
            User.Create(
                email : "handren@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.AdminRoleId
            ),
            User.Create(
                email : "sazan@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.AdminRoleId
            ),
            User.Create(
                email : "member@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.MemberRoleId
            ),
            User.Create(
                email : "liberian@mail.com",
                password: passwordHasher.HashPassword(null, "password"),
                roleId : RoleAndPermissionSeeder.LibrarianRoleId
            ),
        };

        modelBuilder.Entity<User>().HasData(users);
    }
}