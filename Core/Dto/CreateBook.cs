using Core.Entity;

namespace Core.Dto;

public record CreateBook(string Isbn, string Title, Guid CategoryId, string Author, int AvailableCopies)
{
    public Book ToBook()
    {
        return Book.CreateBook(
            title: Title,
            isbn: Isbn,
            category: new CategoryId(CategoryId),
            author: Author,
            availableCopies: AvailableCopies
        );
    }
};