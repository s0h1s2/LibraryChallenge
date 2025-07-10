using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class PermissionSeeder:IDataSeeder
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var permissions = new List<PermissionEntity>();
        var permissionId = 1;
        // Admin permissions - all permissions
        foreach (var perm in Enum.GetValues<AttributeType>())
        {
            permissions.Add(new PermissionEntity
            {
                Id = permissionId,
                Name = perm.ToString(),
                RoleId = RoleSeeder.AdminRoleId
            });
            permissionId++;
        }

        // Librarian permissions
        var librarianPermissions = new[]
        {
            AttributeType.CanDeleteBooks,
            AttributeType.CanCreateBooks,
            AttributeType.CanReturnBooks,
            AttributeType.CanUpdateBooks,
            AttributeType.CanBorrowBooks,
            AttributeType.CanExtendDueDate
        };

        foreach (var perm in librarianPermissions)
        {
            permissions.Add(new PermissionEntity
            {
                Id = permissionId,
                Name = perm.ToString(),
                RoleId = RoleSeeder.LibrarianRoleId
            });
            permissionId++;
        }

        // Member permissions
        var memberPermissions = new[]
        {
            AttributeType.CanBorrowBooks,
            AttributeType.CanReturnBooks
        };

        foreach (var perm in memberPermissions)
        {
            permissions.Add(new PermissionEntity
            {
                Id =permissionId,
                Name = perm.ToString(),
                RoleId = RoleSeeder.MemberRoleId
            });
            permissionId++;
        }

        modelBuilder.Entity<PermissionEntity>().HasData(permissions);
    }
}