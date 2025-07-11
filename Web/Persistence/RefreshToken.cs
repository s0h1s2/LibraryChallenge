using Core.Entity;

namespace Web.Persistence;

public class RefreshToken
{
    private RefreshToken(string token, DateTime expiration, Guid userId)
    {
        Id = Guid.NewGuid();
        Token = token;
        Expiration = expiration;
        UserId = userId;
    }

    public Guid Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime Expiration { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; } = null!;

    public static RefreshToken Create(string token, DateTime expiration, Guid userId)
    {
        return new RefreshToken(token, expiration, userId);
    }
}