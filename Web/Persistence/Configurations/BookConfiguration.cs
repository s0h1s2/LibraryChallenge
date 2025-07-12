using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Util.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever();
        builder.Property(b => b.Isbn)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.AvailableCopies)
            .IsRequired();
        builder
            .Property(b => b.TotalCopies)
            .IsRequired();

        builder.Property(b => b.CategoryId).HasColumnName("CategoryId");

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey("CategoryId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}