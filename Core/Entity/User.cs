namespace Core.Entity;

public class User
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public Guid Id { get; private set; }
    public Role Role { get; private set; }
    
    private User(string username, string password)
    {
        Username = username;
        Password = password;
        Id = Guid.NewGuid();
    }
    public static User Create(string username, string password)
    {
        return new User(username,password);
    }

    public void AssignRole(Role role)
    {
        Role = role;
    }
}