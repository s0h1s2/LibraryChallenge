using Core.Dto;
using Core.Persistance;
using Core.ValueObjects;
using FluentValidation;

namespace Web.Util.Validations;

public class CreateUserValidation : AbstractValidator<CreateUser>
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidation(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        return await _userRepository.IsEmailUniqueAsync(email);
    }

    private bool BeValidRole(string roleName)
    {
        return Enum.TryParse<RoleType>(roleName, out _);
    }
}