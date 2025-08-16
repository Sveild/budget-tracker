using MediatR;

namespace BudgetTracker.API.Controllers;

public abstract class BaseSenderController : BaseController
{
    protected readonly ISender _sender;

    protected BaseSenderController(ISender sender)
    {
        _sender = sender;
    }
}
