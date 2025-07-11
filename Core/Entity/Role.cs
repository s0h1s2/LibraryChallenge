using Core.ValueObjects;

namespace Core.Entity;

public class Role
{
    public RoleType Name { get; private set; }
    public int Id { get; private set; }
    public HashSet<RolePermission> Permissions { get; private set; }
    public string RoleName => Name.ToString();
    
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
}