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
    private Category(Guid id, string name)
    {
        if (id == Guid.Empty) throw new DomainException("Id cannot be empty");
        Id = id;
        Name = name;
    }
    public static Category Create(string name)
    {
        return new Category(name);
    }
    public static Category CreateExisting(Guid guid,string name)
    {
        return new Category(guid,name);
    }
}