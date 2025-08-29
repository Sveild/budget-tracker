using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using BudgetTracker.Application.Common.RequestHandlers;
using MediatR;

namespace BudgetTracker.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler
    : BaseRequestHandler,
        IRequestHandler<UpdateCategoryCommand, Domain.Entities.Category>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Category> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = GuardAgainstNotFound(
            request.Id,
            await _context.Categories.FindAsync([request.Id], cancellationToken)
        );

        category.Name = request.Name;
        category.Icon = request.Icon;
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }
}
