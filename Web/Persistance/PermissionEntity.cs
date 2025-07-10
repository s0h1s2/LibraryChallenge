using Core.Entity;

namespace Web.Persistance;

public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
}