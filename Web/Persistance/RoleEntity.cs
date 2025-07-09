namespace Web.Persistance;

public class RoleEntity
{
    public int Id { get; set; }
    public ICollection<PermissionEntity> Permissions { get; set; }
}