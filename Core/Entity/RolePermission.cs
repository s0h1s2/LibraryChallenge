using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class RolePermission
{
    public int Id { get; private set; }
    private PermissionType _permission;
    public int RoleId;
    public Role Role;
    public string Name => _permission.ToString();
    private RolePermission() { }
    private RolePermission(PermissionType permission)
    {
        _permission = permission;
    }
    public static RolePermission Create (PermissionType type)
    {
        return new RolePermission(type);
    }
}