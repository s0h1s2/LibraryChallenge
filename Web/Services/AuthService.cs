using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Controllers;
using Web.Exceptions;
using Web.Persistence;
using Web.Util;

namespace Web.Services;

public class AuthService
{
    private ApplicationDbContext _dbContext;
    private TokenProvider _tokenProvider;

    public async Task<LoginUserResponse> LoginUser(string email, string password)
    {
        var user = await _dbContext.User.Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        if (user is null) throw new UnauthorizedException(Messages.InvalidLogin);

        var passwordHasher = new PasswordHasher<object?>();
        var result = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed) throw new UnauthorizedException(Messages.InvalidLogin);

        var token = _tokenProvider.Create(user);
        var (refreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(RefreshToken.Create(refreshToken, expirationDate, user.Id));
        await _dbContext.SaveChangesAsync();

        return new LoginUserResponse(token, refreshToken);
    }

    public async Task<LoginUserResponse> RefreshUserToken(string token)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Where(rt => rt.Token == token)
            .FirstOrDefaultAsync();

        if (refreshToken is null || refreshToken.Expiration < DateTime.UtcNow)
            throw new UnauthorizedException(Messages.NotFoundMessage);

        var user = await _dbContext.User.FindAsync(refreshToken.UserId);
        if (user is null) new UnauthorizedException(Messages.NotFoundMessage);

        var newToken = _tokenProvider.Create(user);
        var (newRefreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(RefreshToken.Create(newRefreshToken, expirationDate, user.Id));
        _dbContext.RefreshTokens.Remove(refreshToken);

        await _dbContext.SaveChangesAsync();
        return new LoginUserResponse(newToken, newRefreshToken);
    }

    public record LoginUserResponse(string Token, string RefreshToken);
}