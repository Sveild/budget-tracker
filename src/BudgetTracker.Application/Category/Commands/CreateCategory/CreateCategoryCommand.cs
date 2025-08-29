using MediatR;

namespace BudgetTracker.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Domain.Entities.Category>
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}
