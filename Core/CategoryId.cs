namespace Core;

public class CategoryId(Guid id)
{
    public Guid Id { get; private set; } = id;
}