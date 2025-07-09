using Core.ValueObjects;

namespace Core.Entity;

public class Role
{
    public RoleType Name { get; private set; }
    public Guid Id { get; private set; }
    private HashSet<RoleAttribute> _attributes = new();

    private Role(RoleType name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public static Role Create(RoleType roleType)
    {
        return new Role(roleType);
    }

    public void AssignAttribute(RoleAttribute attribute)
    {
        _attributes.Add(attribute);
    }

    public bool HasPermission(RoleAttribute attribute)
    {
        return _attributes.Contains(attribute);
    }

    public void RevokeAttribute(RoleAttribute attribute)
    {
        if (_attributes.Contains(attribute))
        {
            _attributes.Remove(attribute);
        }
    }
}