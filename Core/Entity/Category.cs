namespace Core;

public class Category
{
    public CategoryId Id { get; private set; }
    public string Name { get; private set; }

    private Category(string name)
    {
        Id = new CategoryId(Guid.NewGuid());
        Name = name;
    }
    private Category(CategoryId categoryId, string name)
    {
        Id = categoryId;
        Name = name;
    }
    public static Category Create(string name)
    {
        return new Category(name);
    }
    public static Category CreateExisting(CategoryId categoryId,string name)
    {
        return new Category(categoryId,name);
    }
}