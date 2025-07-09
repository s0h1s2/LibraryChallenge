using System.Net;
using Core.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Persistance;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController:BaseController
{
    private readonly ApplicationDbContext _dbContext;

    public UsersController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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