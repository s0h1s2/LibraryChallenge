using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class RolePermission
{
    public int Id { get; private set; }
    private PermissionType _permission;
    public int RoleId { get; private set; }
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
    public void AssignToRole(int roleId)
    {
        if (roleId <= 0) throw new ArgumentException("Role ID must be greater than zero");
        RoleId = roleId;
    }
}