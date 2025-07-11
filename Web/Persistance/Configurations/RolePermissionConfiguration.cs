using Core.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Persistance.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<Core.Entity.RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(rp => rp.Id);
        builder.Property(rp => rp.Id)
            .ValueGeneratedOnAdd();

        builder.Ignore(rp => rp.Name);

        builder.Property("_permission")
            .HasConversion<string>()
            .HasColumnName("PermissionType")
            .HasMaxLength(100)
            .IsRequired();

    }
}