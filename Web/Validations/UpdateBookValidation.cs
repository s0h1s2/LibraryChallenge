using Core.Dto;
using Core.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Persistence;

namespace Web.Validations;

public class UpdateBookValidation : AbstractValidator<UpdateBook>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateBookValidation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Title)
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Author)
            .MaximumLength(50).WithMessage("Author cannot exceed 50 characters.");
        RuleFor(x => x.CategoryId)
            .MustAsync(LookForCategory)
            .When(x => x.CategoryId.Id != Guid.Empty)
            .WithMessage("Category is required.");
        RuleFor(x => x.AvailableCopies);
    }

    private Task<bool> LookForCategory(CategoryId categoryId, CancellationToken cancellationToken)
    {
        return _dbContext.Category.AnyAsync(entity => entity.Id == categoryId, cancellationToken);
    }
}