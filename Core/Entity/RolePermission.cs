namespace Core.Entity;

public class RolePermission
{
    private RolePermission()
    {
    }

    private RolePermission(Role role, Permission permission)
    {
        Id = Guid.NewGuid();
        Role = role ?? throw new ArgumentNullException(nameof(role), "Role cannot be null");
        Permission = permission ?? throw new ArgumentNullException(nameof(permission), "Permission cannot be null");
        RoleId = role.Id;
        PermissionId = permission.Id;
    }

    public Guid Id { get; private set; }
    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
    public Role Role { get; private set; }
    public Permission Permission { get; private set; }

    public static RolePermission Create(Permission permission, Role role)
    {
        return new RolePermission(role, permission);
    }
}