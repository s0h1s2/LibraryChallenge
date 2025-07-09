namespace Web.Persistance;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public ICollection<PermissionEntity> Permissions { get; set; }
}