using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Util.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        // Primary key
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasConversion<string>() // Store enum as string
            .HasMaxLength(50);

        builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity(j => j.ToTable("RolePermissions"));
    }
}