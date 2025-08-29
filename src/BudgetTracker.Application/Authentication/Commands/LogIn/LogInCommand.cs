using MediatR;

namespace BudgetTracker.Application.Authentication.Commands.LogIn;

public class LogInCommand : IRequest<LogInResult>
{
    public string UsernameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
}
