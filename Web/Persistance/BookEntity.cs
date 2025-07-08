namespace Web.Persistance;

public class BookEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    
    public int AvailableCopies { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }
    
}