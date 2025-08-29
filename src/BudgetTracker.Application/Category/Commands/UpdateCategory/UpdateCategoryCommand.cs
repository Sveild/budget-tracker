using System;
using MediatR;

namespace BudgetTracker.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Domain.Entities.Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Icon { get; set; } = String.Empty;
}
