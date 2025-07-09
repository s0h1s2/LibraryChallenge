namespace Core.Dto;

public record UpdateBook(
    string Title,
    string Isbn,
    Guid CategoryId,
    string Author,
    int AvailableCopies
);
