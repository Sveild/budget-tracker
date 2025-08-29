using System;
using MediatR;

namespace BudgetTracker.Application.Category.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public Guid Id { get; set; }
}
