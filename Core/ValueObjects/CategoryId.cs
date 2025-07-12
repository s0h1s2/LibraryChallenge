namespace Core;

public class CategoryId
{
    public CategoryId(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}