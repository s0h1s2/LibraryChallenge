using Core;
using Core.Dto;
using Core.Persistance;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Web.Persistance;

namespace Web.Validations;

public class CreateBookValidation : AbstractValidator<CreateBook>
{
    private readonly ApplicationDbContext _dbContext;
    public CreateBookValidation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Author)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .MinimumLength(13)
            .MaximumLength(13);
        RuleFor(x => x.CategoryId).MustAsync(LookForCategory);

    }

    private async Task<bool> LookForCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Category.AnyAsync(c => c.Id == new CategoryId(categoryId), cancellationToken);
    }

}