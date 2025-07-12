using Core.Dto;
using Core.Persistance;
using FluentValidation;

namespace Web.Validations;

public class RegisterUserValidation : AbstractValidator<RegisterUser>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserValidation(IUserRepository userRepository)
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
    }

    private async Task<bool> CheckForUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.IsEmailUniqueAsync(email);
    }
}