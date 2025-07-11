using Core.Dto;
using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Authorization;
using Web.Persistence;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class UsersController : BaseController
{
    private readonly ApplicationDbContext _dbContext;

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
        var role = await _dbContext.Role
            .Where(r => r.Name.ToString() == newUserRequest.RoleType)
            .FirstOrDefaultAsync();

        var passwordHasher = new PasswordHasher<object?>();
        var hashedPassword = passwordHasher.HashPassword(null, newUserRequest.Password);

        var newUser = Core.Entity.User.Create(newUserRequest.Email, hashedPassword, role);
        _dbContext.User.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{userId:guid}", Name = "Update User")]
    [HasPermission(PermissionType.CanUpdateUsers)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUser updateUserRequest)
    {
        var user = await _dbContext.User
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        var role = await _dbContext.Role
            .Where(r => r.Name.ToString() == updateUserRequest.Role.ToString())
            .FirstOrDefaultAsync();
        if (user is null || role is null)
        {
            return NotFound("User or role not found");
        }

        var passwordHasher = new PasswordHasher<object?>();
        var hashedPassword = passwordHasher.HashPassword(null, updateUserRequest.Password);
        user.UpdateInfo(new UpdateUser(
            updateUserRequest.Email,
            hashedPassword,
            role
        ));
        _dbContext.User.Update(user);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    record UserResponse(Guid Id, string Email, string RoleType);
}