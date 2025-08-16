using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Application.Common.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public LoggingBehavior(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id;
        string? userName = string.Empty;

        if (userId != Guid.Empty)
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation(
            "CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName,
            userId,
            userName,
            request
        );
    }
}
