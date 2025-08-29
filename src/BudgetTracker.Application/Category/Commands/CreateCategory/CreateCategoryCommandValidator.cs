using FluentValidation;

namespace BudgetTracker.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
