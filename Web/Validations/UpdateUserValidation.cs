using Core.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Controllers;
using Web.Persistence;

namespace Web.Validations;

public class UpdateUserValidation : AbstractValidator<UsersController.UpdateUserRequest>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateUserValidation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Email)
            .EmailAddress()
            .MustAsync(CheckForUniqueEmail)
            .WithMessage("Email must be unique");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.RoleType)
            .Must(BeValidRole)
            .When(x => !string.IsNullOrEmpty(x.RoleType))
            .WithMessage("RoleType must be one of the following: " + string.Join(", ", Enum.GetNames<RoleType>()));
    }

    private async Task<bool> CheckForUniqueEmail(string email, CancellationToken cancellation)
    {
        return await _dbContext.User.AnyAsync(user => user.Email == email, cancellation) is false;
    }

    private bool BeValidRole(string roleName)
    {
        return Enum.TryParse<RoleType>(roleName, out _);
    }
}