using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using MediatR;

namespace BudgetTracker.Application.Authentication.Commands.LogIn;

public class LogInCommandHandler : IRequestHandler<LogInCommand, LogInResult>
{
    private IIdentityService _identityService;

    public LogInCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LogInResult> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.SignInAsync(request.UsernameOrEmail, request.Password);

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new UnauthorizedAccessException("Invalid username/email or password");
        }

        return new LogInResult { Token = token };
    }
}
