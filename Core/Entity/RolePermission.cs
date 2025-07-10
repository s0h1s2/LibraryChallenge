using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class RolePermission
{
    public Guid Id { get; private set; }
    private PermissionType _permission;
    public string Name => _permission.ToString();
    private RolePermission(PermissionType permission)
    {
        Id= Guid.NewGuid();
        _permission = permission;
    }
    public static RolePermission Create (PermissionType type)
    {
        return new RolePermission(type);
    }
}