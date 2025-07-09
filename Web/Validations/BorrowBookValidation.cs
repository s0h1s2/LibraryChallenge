using Core.Dto;
using FluentValidation;

namespace Web.Validations;

public class BorrowBookValidation:AbstractValidator<BorrowBook>
{
    public BorrowBookValidation()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID cannot be empty.")
            .NotEqual(Guid.Empty)
            .WithMessage("Book ID cannot be an empty GUID.");

        RuleFor(x => x.BorrowDate)
            .NotEmpty()
            .WithMessage("Borrow date cannot be empty.");
    }
}