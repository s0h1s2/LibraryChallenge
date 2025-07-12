using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Util.Persistence.Configurations;

public class BorrowedBooksConfiguration : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.ToTable("BorrowedBooks");

        builder.HasKey(bb => bb.Id);

        builder.Property(bb => bb.Id)
            .ValueGeneratedNever();

        builder.Property(bb => bb.DueDate)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(bb => bb.ReturnDate)
            .HasColumnType("timestamp without time zone");


        builder.HasOne(bb => bb.Book)
            .WithMany()
            .HasForeignKey(bb => bb.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bb => bb.User)
            .WithMany(u => u.BorrowedBooks)
            .HasForeignKey(bb => bb.UserId);
    }
}