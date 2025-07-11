using Core.ValueObjects;

namespace Core.Entity;

public class Permission
{
    private readonly PermissionType _permission;

    private Permission()
    {
    }

    private Permission(PermissionType permission)
    {
        _permission = permission;
    }

    private Permission(int id, PermissionType type)
    {
        Id = id;
        _permission = type;
    }

    public int Id { get; private set; }
    public string Name => _permission.ToString();
    public IEnumerable<RolePermission> RolePermissions { get; }

    public static Permission Create(PermissionType type)
    {
        return new Permission(type);
    }

    public static Permission CreateExisting(int id, PermissionType type)
    {
        return new Permission(id, type);
    }
}