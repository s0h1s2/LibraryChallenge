using Core.Entity;
using Core.ValueObjects;

namespace Core.Dto;

public record CreateBook(
    string Isbn,
    string Title,
    Guid CategoryId,
    string Author,
    int AvailableCopies,
    int totalCopies)
{
    public Book ToBook()
    {
        return Book.CreateBook(
            title: Title,
            isbn: Isbn,
            category: new CategoryId(CategoryId),
            author: Author,
            availableCopies: AvailableCopies,
            totalCopies: totalCopies
        );
    }
}