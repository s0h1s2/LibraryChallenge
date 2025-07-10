using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Persistance;

public class BookConfiguration:IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever();
        builder.Property(b => b.Isbn)
            .IsRequired()
            .HasMaxLength(13);
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.AvailableCopies)
            .IsRequired();
        builder.Property(b => b.CategoryId).HasConversion(cate=> cate.Id, val => new CategoryId(val));
        
        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("Books");
    }
}