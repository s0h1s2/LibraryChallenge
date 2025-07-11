using Core.Dto;
using Core.ValueObjects;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Persistance;
using Web.Util;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenProvider _tokenProvider;
    private record LoginUserResponse(string Token, string RefreshToken);
    public record RefreshTokenRequest(string RefreshToken);


    public AuthController(ApplicationDbContext dbContext, TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("login", Name = "Login User")]
    [Produces(typeof(ApiResponse<LoginUserResponse>))]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
    {
        var user = await _dbContext.User.Where(u => u.Email == loginUser.Email)
            .FirstOrDefaultAsync();
        if (user is null)
        {
            return Failure("Invalid email or password", 401);
        }
        var passwordHasher = new PasswordHasher<object?>();
        var result = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginUser.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return Failure("Invalid email or password", 401);
        }

        var token = _tokenProvider.Create(user);
        var (refreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(Persistance.RefreshToken.Create(refreshToken, expirationDate, user.Id));
        await _dbContext.SaveChangesAsync();

        return Success(new LoginUserResponse(token, refreshToken));
    }
    [HttpPost("refresh", Name = "Refresh JWT Token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Where(rt => rt.Token == request.RefreshToken)
            .FirstOrDefaultAsync();

        if (refreshToken is null || refreshToken.Expiration < DateTime.UtcNow)
        {
            return Failure("Invalid or expired refresh token", 401);
        }

        var user = await _dbContext.User.FindAsync(refreshToken.UserId);
        if (user is null)
        {
            return Failure("User not found", 404);
        }

        var newToken = _tokenProvider.Create(user);
        var (newRefreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(Persistance.RefreshToken.Create(newRefreshToken, expirationDate, user.Id));

        await _dbContext.SaveChangesAsync();

        return Success(new LoginUserResponse(newToken, newRefreshToken));
    }
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUser registerUser)
    {
        var memberRole = await _dbContext.Role
            .Where(role => role.Name == RoleType.Member)
            .FirstOrDefaultAsync();
        _dbContext.User.Add(Core.Entity.User.Create(registerUser.Email, new PasswordHasher<object?>().HashPassword(null, registerUser.Password), memberRole.Id));

        await _dbContext.SaveChangesAsync();
        return Success(new
        {
            Email = registerUser.Email,
        }, status: 201);
    }



}