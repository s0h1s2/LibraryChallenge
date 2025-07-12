using System.Diagnostics;
using Core.Dto;
using Core.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Persistence;
using Web.Util;
using UserEntity = Core.Entity.User;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenProvider _tokenProvider;

    public AuthController(ApplicationDbContext dbContext, TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("login", Name = "Login User")]
    public async Task<Results<UnauthorizedHttpResult, Ok<SuccessResponse<LoginUserResponse>>>> LoginUser(
        [FromBody] LoginUser loginUser)
    {
        var user = await _dbContext.User.Where(u => u.Email == loginUser.Email)
            .FirstOrDefaultAsync();
        if (user is null) return TypedResults.Unauthorized();

        var passwordHasher = new PasswordHasher<object?>();
        var result = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginUser.Password);
        if (result == PasswordVerificationResult.Failed) return TypedResults.Unauthorized();

        var token = _tokenProvider.Create(user);
        var (refreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(Persistence.RefreshToken.Create(refreshToken, expirationDate, user.Id));
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(new SuccessResponse<LoginUserResponse>(new LoginUserResponse(token, refreshToken)));
    }

    [HttpPost("refresh", Name = "Refresh JWT Token")]
    public async Task<Results<UnauthorizedHttpResult, Ok<SuccessResponse<LoginUserResponse>>>> RefreshToken(
        [FromBody] RefreshTokenRequest request)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Where(rt => rt.Token == request.RefreshToken)
            .FirstOrDefaultAsync();

        if (refreshToken is null || refreshToken.Expiration < DateTime.UtcNow)
            return TypedResults.Unauthorized();

        var user = await _dbContext.User.FindAsync(refreshToken.UserId);
        if (user is null) return TypedResults.Unauthorized();


        var newToken = _tokenProvider.Create(user);
        var (newRefreshToken, expirationDate) = _tokenProvider.CreateRefreshToken(user);
        _dbContext.RefreshTokens.Add(Persistence.RefreshToken.Create(newRefreshToken, expirationDate, user.Id));

        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(
            new SuccessResponse<LoginUserResponse>(new LoginUserResponse(newToken, newRefreshToken)));
    }

    [HttpPost("register", Name = "Register Normal User")]
    public async Task<Ok<RegisterUserResponse>> CreateUser([FromBody] RegisterUser registerUser)
    {
        var memberRole = await _dbContext.Role
            .Where(role => role.Name == RoleType.Member)
            .FirstOrDefaultAsync();
        Debug.Assert(memberRole is not null, "memberRole is null");

        _dbContext.User.Add(UserEntity.Create(registerUser.Email,
            new PasswordHasher<object?>().HashPassword(null, registerUser.Password), memberRole));

        await _dbContext.SaveChangesAsync();
        return TypedResults.Ok(new RegisterUserResponse(registerUser.Email));
    }

    public record LoginUserResponse(string Token, string RefreshToken);

    public record RefreshTokenRequest(string RefreshToken);

    public record RegisterUserResponse(string Email);
}