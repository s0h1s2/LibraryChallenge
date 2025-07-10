using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class RoleAttribute
{
    private PermissionType _permission;

    private RoleAttribute(PermissionType permission)
    {
        _permission = permission;
    }
    public static RoleAttribute Create (PermissionType type)
    {
        return new RoleAttribute(type);
    }
}