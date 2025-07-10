using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Web.Persistance;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Web.Util;

public sealed class TokenProvider(IConfiguration configuration)
{
    public string Create(UserEntity user)
    {
        var secretKey = configuration["Jwt:Key"] ?? string.Empty;
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Unicode.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
            ]),
            Expires = DateTime.UtcNow.AddSeconds(configuration.GetValue<int>("Jwt:ExpireInSeconds", 3600)),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }
}