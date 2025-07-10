using Core.Entity;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class RoleAndPermissionSeeder : IDataSeeder
{
    public static int AdminRoleId { get; private set; } = (int)RoleType.Admin;
    public static int LibrarianRoleId { get; private set; } = (int)RoleType.Liberian;
    public static int MemberRoleId { get; private set; } = (int)RoleType.Member;

    public static void Configure(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedRolePermissions(modelBuilder);
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = GetRoleDefinitions()
            .Select(roleDefinition => Role.CreateExisting(roleDefinition.Id, roleDefinition.RoleType))
            .ToArray();

        modelBuilder.Entity<Role>().HasData(roles);
    }

    private static void SeedRolePermissions(ModelBuilder modelBuilder)
    {
        var rolePermissions = GetRolePermissionMappings()
            .SelectMany(CreateRolePermissionsForRole)
            .Select((permission, index) => CreateRolePermissionWithId(index + 1, permission.Permission, permission.RoleId))
            .ToArray();

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }

    private static RolePermission CreateRolePermissionWithId(int id, PermissionType permission, int roleId)
    {
        var rolePermission = RolePermission.CreateExisting(id, permission);
        rolePermission.AssignToRole(roleId);
        return rolePermission;
    }

    private static IEnumerable<(int Id, RoleType RoleType)> GetRoleDefinitions() =>
        new[]
        {
            ((int)RoleType.Admin, RoleType.Admin),
            ((int)RoleType.Liberian, RoleType.Liberian),
            ((int)RoleType.Member, RoleType.Member)
        };

    private static IEnumerable<(RoleType Role, PermissionType[] Permissions)> GetRolePermissionMappings() =>
        new[]
        {
            (RoleType.Admin, GetAllPermissions()),
            (RoleType.Liberian, GetLibrarianPermissions()),
            (RoleType.Member, GetMemberPermissions())
        };

    private static IEnumerable<(PermissionType Permission, int RoleId)> CreateRolePermissionsForRole(
        (RoleType Role, PermissionType[] Permissions) roleMapping) =>
        roleMapping.Permissions.Select(permission => (permission, (int)roleMapping.Role));

    private static PermissionType[] GetAllPermissions() =>
        Enum.GetValues<PermissionType>();

    private static PermissionType[] GetLibrarianPermissions() =>
        new[]
        {
            PermissionType.CanDeleteBooks,
            PermissionType.CanCreateBooks,
            PermissionType.CanReturnBooks,
            PermissionType.CanUpdateBooks,
            PermissionType.CanBorrowBooks,
            PermissionType.CanExtendDueDate
        };

    private static PermissionType[] GetMemberPermissions() =>
        new[]
        {
            PermissionType.CanBorrowBooks,
            PermissionType.CanReturnBooks
        };
}