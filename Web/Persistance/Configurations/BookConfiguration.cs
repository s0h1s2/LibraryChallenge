using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Web.Persistance.Configurations;

internal class BookConfiguration:IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).HasMaxLength(100);
        builder.Property(c=>c.Author).HasMaxLength(100);
        builder.Property(c=>c.Isbn).HasMaxLength(13);
        // Configure the CategoryId value object
        builder.Property(b => b.CategoryId)
            .HasConversion(
                categoryId => categoryId.Id,
                guid => new CategoryId(guid)
            )
            .HasColumnName("CategoryId")
            .IsRequired();

    }
}