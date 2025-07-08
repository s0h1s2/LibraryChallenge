namespace Core;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public static Category Create(string name)
    {
        return new Category(name);
    }
}