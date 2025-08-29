using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using BudgetTracker.Application.Common.RequestHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Application.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : BaseRequestHandler, IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = GuardAgainstNotFound(
            request.Id,
            await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
        );

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
