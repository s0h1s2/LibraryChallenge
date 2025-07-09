using System.Net;
using Core.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Persistance;
using Web.Util;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController:BaseController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TokenProvider _tokenProvider;

    public UsersController(ApplicationDbContext dbContext,TokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    
    [HttpPost("auth",Name="Login User")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
    {
        var user = await _dbContext.User.Where(u=>u.Email== loginUser.Email)
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
        return Success(new
        {
            Token = _tokenProvider.Create(user)
        });
    }
    [HttpPost("")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser createUser)
    {
        _dbContext.User.Add(new UserEntity()
        {
            Email = createUser.Email,
            PasswordHash = new PasswordHasher<object?>().HashPassword(null,createUser.Password)
        });
        await _dbContext.SaveChangesAsync();
        return Success(new
        {
            Email = createUser.Email,
        },status:201);
    }
}