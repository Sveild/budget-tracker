using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using MediatR;

namespace BudgetTracker.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Domain.Entities.Category>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Category> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = new Domain.Entities.Category { Name = request.Name, Icon = request.Icon };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }
}
