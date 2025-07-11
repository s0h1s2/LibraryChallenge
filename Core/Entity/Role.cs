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
    public HashSet<RolePermission> Permissions { get; private set; } = new HashSet<RolePermission>();


    public static Role Create(RoleType roleType)
    {
        return new Role(roleType);
    }

    public static Role CreateExisting(int id, RoleType roleType)
    {
        return new Role(id, roleType);
    }

    public void AssignAttribute(RolePermission permission)
    {
        Permissions.Add(permission);
    }

    public bool HasPermission(RolePermission permission)
    {
        return Permissions.Contains(permission);
    }

    public void RevokeAttribute(RolePermission permission)
    {
        if (Permissions.Contains(permission))
        {
            Permissions.Remove(permission);
        }
    }

    public void AssignPermission(RolePermission permission)
    {
        if (permission is null) throw new ArgumentNullException(nameof(permission), "Role permission cannot be null");

        Permissions.Add(permission);
    }

    public void RevokePermission(RolePermission permission)
    {
        var permissionToRemove = Permissions.FirstOrDefault(perm => perm.Id == perm.Id);
        if (permissionToRemove is null)
        {
            throw new DomainException("Permission does not exist in the role.");
        }

        Permissions.Remove(permissionToRemove);
    }

    public void AssignPermissions(IList<RolePermission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            AssignPermission(rolePermission);
        }
    }

    public void RevokePermissions(List<RolePermission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            RevokePermission(rolePermission);
        }
    }
}