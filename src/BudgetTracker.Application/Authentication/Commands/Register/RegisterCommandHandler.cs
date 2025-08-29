using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using BudgetTracker.Application.Common.Models;
using MediatR;

namespace BudgetTracker.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        (Result result, Guid id) = await _identityService.CreateUserAsync(
            request.Username,
            request.Email,
            request.Password
        );

        return result;
    }
}
