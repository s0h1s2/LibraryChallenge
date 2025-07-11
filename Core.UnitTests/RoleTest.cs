using Core.Entity;
using Core.ValueObjects;

namespace Core.UnitTests;

public class RoleTest : IDisposable
{
    private readonly Role _role;

    public RoleTest()
    {
        _role = Role.Create(RoleType.Admin);
    }

    public void Dispose()
    {
    }

    [Fact]
    public void TestAssignRolePermission_To_Role_Role_Must_Have_Permission()
    {
        var permission = RolePermission.Create(PermissionType.CanBorrowBooks);

        _role.AssignPermission(permission);

        Assert.Contains(permission, _role.Permissions);
    }

    [Fact]
    public void TestAssignRolePermission_To_Role_Role_Must_Throw_Exception_When_Permission_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => _role.AssignPermission(null));
    }

    [Fact]
    public void TestRevokeRolePermission_From_Role_Role_Must_Not_Have_Permission()
    {
        var permission = RolePermission.Create(PermissionType.CanBorrowBooks);
        _role.AssignPermission(permission);

        _role.RevokePermission(permission);

        Assert.DoesNotContain(permission, _role.Permissions);
    }

    [Fact]
    public void TestRevokeRolePermission_From_Role_Should_Throw_Domain_Exception_When_Permission_Doesnt_Exist()
    {
        var permission = RolePermission.Create(PermissionType.CanBorrowBooks);
        Assert.Throws<DomainException>(() => _role.RevokePermission(permission));
    }

    [Fact]
    public void TestAssignMultiplePermissions_To_Role_Role_Must_Have_All_Permissions()
    {
        var permission1 = RolePermission.Create(PermissionType.CanBorrowBooks);
        var permission2 = RolePermission.Create(PermissionType.CanReturnBooks);
        _role.AssignPermissions(new List<RolePermission> { permission1, permission2 });

        Assert.Contains(permission1, _role.Permissions);
        Assert.Contains(permission2, _role.Permissions);
    }

    [Fact]
    public void TestRevokeMultiplePermissions_To_Role_Role_Shouldnt_Have_Roles()
    {
        var permission1 = RolePermission.Create(PermissionType.CanBorrowBooks);
        var permission2 = RolePermission.Create(PermissionType.CanReturnBooks);
        _role.AssignPermissions(new List<RolePermission> { permission1, permission2 });
        _role.RevokePermissions(new List<RolePermission> { permission1, permission2 });
        Assert.Empty(_role.Permissions);
    }
}