namespace Core;

public class CategoryId
{
    public Guid Id { get; private set; }

    public CategoryId(Guid id)
    {
        Id = id;
    }
}
