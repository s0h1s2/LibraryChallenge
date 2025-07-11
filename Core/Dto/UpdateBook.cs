namespace Core.Dto;

public record UpdateBook(
    string? Title,
    string? Isbn,
    CategoryId? CategoryId,
    string? Author,
    int? AvailableCopies
);