using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Util.Controllers;
using Web.Util.Persistence;

namespace Web.Util.Validations;

public class ManageRolePermissionsValidation : AbstractValidator<RoleManagementController.ManageRolePermissionsRequest>
{
    private readonly ApplicationDbContext _dbContext;

    public ManageRolePermissionsValidation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.AllowedPermissions)
            .MustAsync(CheckIfPermissionsExist)
            .WithMessage("Allowed permissions must be valid and exist in the system.");
        RuleFor(x => x.DeniedPermissions)
            .MustAsync(CheckIfPermissionsExist)
            .WithMessage("Denied permissions must be valid and exist in the system.");
    }

    private async Task<bool> CheckIfPermissionsExist(IEnumerable<string> rolePermissions,
        CancellationToken cancellationToken)
    {
        if (!rolePermissions.Any()) return true;
        return (await _dbContext.Permission.ToListAsync(cancellationToken)).Any(x => rolePermissions.Contains(x.Name));
    }
}