using Core.Dto;
using Core.ValueObjects;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Authorization;
using Web.Persistance;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class UsersController : BaseController
{
    private readonly ApplicationDbContext _dbContext;
    record UserResponse(Guid Id, string Email, string RoleType);
    public UsersController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet(Name = "GetUsers")]
    [HasPermission(PermissionType.CanViewUsers)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.User
            .Include(u => u.Role)
            .ToListAsync();
        return Ok(users.Select(u => new UserResponse(u.Id, u.Email, u.Role.Name.ToString())).ToList());
    }
    [HttpGet("{userId:guid}", Name = "GetUser")]
    [HasPermission(PermissionType.CanViewUsers)]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _dbContext.User.Include(u => u.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null) return NotFound();

        return Ok(new UserResponse(user.Id, user.Email, user.Role.Name.ToString()));
    }
    [HttpPost(Name = "Create User")]
    [HasPermission(PermissionType.CanCreateUsers)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser newUserRequest)
    {
        var roleId = await _dbContext.Role
            .Where(r => r.Name.ToString() == newUserRequest.RoleType)
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        var passwordHasher = new PasswordHasher<object?>();
        var hashedPassword = passwordHasher.HashPassword(null, newUserRequest.Password);

        var newUser = Core.Entity.User.Create(newUserRequest.Email, hashedPassword, roleId);
        _dbContext.User.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}