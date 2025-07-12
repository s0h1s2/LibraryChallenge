using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Web.Persistence;

namespace Web.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ApplicationDbContext _context;

    public PermissionAuthorizationHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return;

        if (Guid.TryParse(userId, out var userIdGuid) is false) return;

        var user = await _context.User
            .Where(u => u.Id == userIdGuid)
            .Include(u => u.Role)
            .ThenInclude(u => u.Permissions)
            .FirstAsync();

        if (user.HasPermission(requirement.Permission)) context.Succeed(requirement);
    }
}