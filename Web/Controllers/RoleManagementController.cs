using Core;
using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Util.Authorization;
using Web.Util.Persistence;

namespace Web.Util.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class RoleManagementController : BaseController
{
    private readonly ApplicationDbContext _dbContext;

    public RoleManagementController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("{roleId:int}/manage-permissions", Name = "ManageRolePermissions")]
    [HasPermission(PermissionType.CanManagePermissions)]
    public async Task<Results<Ok<SuccessResponse<object?>>, NotFound<FailureResponse>, BadRequest<FailureResponse>>>
        ManageRolePermissions(int roleId, [FromBody] ManageRolePermissionsRequest request)
    {
        var role = await _dbContext.Role
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId);
        if (role == null)
        {
            return TypedResults.NotFound(new FailureResponse(Messages.NotFoundMessage));
        }

        var allowedPermissions = (await _dbContext.Permission.ToListAsync())
            .Where(p => request.AllowedPermissions.Contains(p.Name))
            .ToList();

        var deniedPermissions = (await
                _dbContext.Permission
                    .ToListAsync())
            .Where(p => request.DeniedPermissions.Contains(p.Name))
            .ToList();
        try
        {
            role.AssignPermissions(allowedPermissions);
            role.RevokePermissions(deniedPermissions);
        }
        catch (DomainException exception)
        {
            return TypedResults.BadRequest(new FailureResponse(exception.Message));
        }

        _dbContext.Role.Update(role);
        await _dbContext.SaveChangesAsync();
        return TypedResults.Ok(new SuccessResponse<object?>(null, Messages.ResoruceUpdated("Role Permissions")));
    }

    public record ManageRolePermissionsRequest(List<string> AllowedPermissions, List<string> DeniedPermissions);
}