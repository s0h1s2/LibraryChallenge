using Core.Dto;
using Core.ValueObjects;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Web.Persistance;

namespace Web.Validations;

public class CreateUserValidation:AbstractValidator<CreateUser>
{
    private readonly ApplicationDbContext _dbContext;
    public CreateUserValidation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .MustAsync(CheckForUniqueEmail)
            .WithMessage("Email must be unique");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.RoleType)
            .NotEmpty()
            .Must(BeValidRole)
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