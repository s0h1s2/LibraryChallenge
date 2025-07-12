using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Util.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

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