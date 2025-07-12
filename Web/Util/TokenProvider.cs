using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Entity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Web.Util;

public sealed class TokenProvider(IConfiguration configuration)
{
    public string Create(User user)
    {
        var secretKey = configuration["Jwt:Key"] ?? string.Empty;
        var securityKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddSeconds(configuration.GetValue("Jwt:ExpireInSeconds", 3600)),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    public (string token, DateTime expirationDate) CreateRefreshToken(User user)
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var refreshToken = Convert.ToBase64String(randomNumber);
        var expirationDate = DateTime.UtcNow.AddDays(30);
        return (refreshToken, expirationDate);
    }
}