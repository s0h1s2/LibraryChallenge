using Core.Dto;
using Core.Persistance;
using Core.ValueObjects;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Authorization;
using Web.Persistance;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class UsersController:BaseController
{
    private readonly ApplicationDbContext _dbContext;
    record UserResponse(Guid Id, string Email,string RoleType);
    public UsersController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet(Name = "GetUsers")]
    [HasPermission(PermissionType.CanViewUsers)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.User
            .Include(u=>u.Role)
            .ToListAsync();
        return Ok(users.Select(u => new UserResponse(u.Id, u.Email,u.Role.RoleName)).ToList());
    }
    [HttpGet("{userId:guid}",Name = "GetUser")]
    [HasPermission(PermissionType.CanViewUsers)]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _dbContext.User.Include(u => u.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if(user is null) return NotFound();
        
        return Ok(new UserResponse(user.Id, user.Email,user.Role.RoleName));
    }
    [HttpGet("{userId:guid}",Name = "Create User")]
    [HasPermission(PermissionType.CanCreateUsers)]
    public async Task<IActionResult> CreateUser(CreateUser user)
    {
        var roleId= await _dbContext.Role
            .Where(r => r.Name== user.RoleType)
            .Select(r => r.Id)
            .FirstOrDefaultAsync();
        
        var newUser= Core.Entity.User.Create(user.Email, user.Password, roleId);
        _dbContext.User.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
}