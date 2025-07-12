using Core.ValueObjects;

namespace Core.Entity;

public class Role
{
    private Role()
    {
    }

    private Role(RoleType name)
    {
        Name = name;
    }

    private Role(int id, RoleType roleType)
    {
        Id = id;
        Name = roleType;
    }

    public RoleType Name { get; private set; }
    public int Id { get; private set; }
    protected virtual List<Permission> _permissions { get; } = new();
    public ICollection<Permission> Permissions => _permissions;


    public static Role Create(RoleType roleType)
    {
        return new Role(roleType);
    }

    public static Role CreateExisting(int id, RoleType roleType)
    {
        return new Role(id, roleType);
    }

    public void AssignPermission(Permission permission)
    {
        if (permission is null) throw new ArgumentNullException(nameof(permission), "Role permission cannot be null");

        _permissions.Add(permission);
    }

    public void RevokePermission(Permission permission)
    {
        var permissionToRemove = _permissions.FirstOrDefault(perm => permission.Id == perm.Id);
        if (permissionToRemove is null) throw new DomainException("Permission does not exist in the role.");

        _permissions.Remove(permissionToRemove);
    }

    public void AssignPermissions(IList<Permission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions) AssignPermission(rolePermission);
    }

    public void RevokePermissions(List<Permission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions) RevokePermission(rolePermission);
    }
}