using System.IdentityModel.Tokens.Jwt;
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
        
        var userId=context.User.Claims.FirstOrDefault(claim=>claim.Type==JwtRegisteredClaimNames.Sub)?.Value;
        if (userId is null)
        {
            return;
        }
        if (Guid.TryParse(userId, out var userIdGuid) is false)
        {
            return;
        }

        var permissions=await _context.User
            .Where(u => u.Id == userIdGuid)
            .Include(u=>u.Role)
            .ThenInclude(r => r.Permissions)
            .FirstAsync();
        if (permissions.Role.Permissions.Any(p => p.Name == requirement.Permission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}