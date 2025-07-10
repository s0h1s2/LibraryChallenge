using Core.Entity;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Web.Persistance.Seeding;

public class RoleAndPermissionSeeder:IDataSeeder
{
    public static int AdminRoleId { get; private set; }
    public static int LibrarianRoleId { get; private set; }
    public static int MemberRoleId { get; private set; }

    public static void Configure(ModelBuilder modelBuilder)
    {
        var adminRole = Role.Create(RoleType.Admin);
        var liberianRole = Role.Create(RoleType.Liberian);
        var memberRole = Role.Create(RoleType.Member);
        
        modelBuilder.Entity<Role>().HasData(
            adminRole,
            liberianRole,
            memberRole
        );
        AdminRoleId = adminRole.Id;
        LibrarianRoleId = liberianRole.Id;
        MemberRoleId = memberRole.Id;
        
        var adminPermissions = Enum.GetValues<PermissionType>().Select(p =>
        {
            var rpm = RolePermission.Create(p);
            rpm.AssignToRole(adminRole.Id);
            return rpm;
        }).ToList();
        
        // Librarian permissions
        var librarianPermissions = new[]
        {
            PermissionType.CanDeleteBooks,
            PermissionType.CanCreateBooks,
            PermissionType.CanReturnBooks,
            PermissionType.CanUpdateBooks,
            PermissionType.CanBorrowBooks,
            PermissionType.CanExtendDueDate
        }.Select(p =>
        {
            var rpm = RolePermission.Create(p);
            rpm.AssignToRole(liberianRole.Id);
            return rpm;
        });
        var memberPermissions = new[]
        {
            PermissionType.CanBorrowBooks,
            PermissionType.CanReturnBooks
        }.Select(p=>
        {
            var rpm = RolePermission.Create(p);
            rpm.AssignToRole(memberRole.Id);
            return rpm;
        });
        modelBuilder.Entity<RolePermission>().HasData(adminPermissions,librarianPermissions,memberPermissions);
        
    }
}