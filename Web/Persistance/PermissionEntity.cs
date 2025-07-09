using Core.Entity;

namespace Web.Persistance;

public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RoleEntity Role { get; set; } = null!;
}