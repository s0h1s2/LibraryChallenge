using System.Diagnostics;
using Core.Dto;
using Core.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Util.Exceptions;
using Web.Util.Persistence;
using Web.Util.Services;
using UserEntity = Core.Entity.User;

namespace Web.Util.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly AuthService _authService;
    private readonly ApplicationDbContext _dbContext;

    public AuthController(ApplicationDbContext dbContext, AuthService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }

    [HttpPost("login", Name = "Login User")]
    public async Task<Results<UnauthorizedHttpResult, Ok<SuccessResponse<AuthService.LoginUserResponse>>>> LoginUser(
        [FromBody] LoginUser loginUser)
    {
        try
        {
            var result = await _authService.LoginUser(loginUser.Email, loginUser.Password);
            return TypedResults.Ok(new SuccessResponse<AuthService.LoginUserResponse>(result));
        }
        catch (UnauthorizedException e)
        {
            return TypedResults.Unauthorized();
        }
    }

    [HttpPost("refresh", Name = "Refresh JWT Token")]
    public async Task<Results<UnauthorizedHttpResult, Ok<SuccessResponse<AuthService.LoginUserResponse>>>> RefreshToken(
        [FromBody] RefreshTokenRequest request)
    {
        try
        {
            var result = await _authService.RefreshUserToken(request.RefreshToken);
            return TypedResults.Ok(
                new SuccessResponse<AuthService.LoginUserResponse>(result));
        }
        catch (UnauthorizedException)
        {
            return TypedResults.Unauthorized();
        }
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


    public record RefreshTokenRequest(string RefreshToken);

    public record RegisterUserResponse(string Email);
}