using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Web.Persistance;

namespace Web.Util;


public class PermissionAuthorizationHandler:AuthorizationHandler<PermissionRequirement>
{
    private readonly ApplicationDbContext _context;

    public PermissionAuthorizationHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId= context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return;
        }
        
        if (Guid.TryParse(userId, out var userIdGuid) is false)
        {
            return;
        }

        var permissions = await _context.User.Where(user => user.Id == userIdGuid)
            .Include(user => user.Role)
            .ThenInclude(role => role.Permissions)
            .FirstAsync();

        if (permissions.Role.Permissions.Any(p => p.Name == requirement.Permission))
        {
            context.Succeed(requirement);
        }

    }
}