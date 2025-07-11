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
    private List<RolePermission> _permissions { get; } = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _permissions.AsReadOnly();
    public IReadOnlyCollection<Permission> Permissions => _permissions.Select(perm => perm.Permission).ToList();


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

        _permissions.Add(RolePermission.Create(permission, this));
    }

    public void RevokePermission(Permission permission)
    {
        var permissionToRemove = _permissions.FirstOrDefault(perm => perm.Id == perm.Id);
        if (permissionToRemove is null)
        {
            throw new DomainException("Permission does not exist in the role.");
        }

        _permissions.Remove(permissionToRemove);
    }

    public void AssignPermissions(IList<Permission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            AssignPermission(rolePermission);
        }
    }

    public void RevokePermissions(List<Permission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            RevokePermission(rolePermission);
        }
    }
}