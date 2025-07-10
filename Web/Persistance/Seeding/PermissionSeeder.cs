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
        foreach (var perm in Enum.GetValues<PermissionType>())
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
            PermissionType.CanDeleteBooks,
            PermissionType.CanCreateBooks,
            PermissionType.CanReturnBooks,
            PermissionType.CanUpdateBooks,
            PermissionType.CanBorrowBooks,
            PermissionType.CanExtendDueDate
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
            PermissionType.CanBorrowBooks,
            PermissionType.CanReturnBooks
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