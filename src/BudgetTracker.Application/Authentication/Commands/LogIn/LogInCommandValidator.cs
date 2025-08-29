using FluentValidation;

namespace BudgetTracker.Application.Authentication.Commands.LogIn;

public class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(command => command.UsernameOrEmail).NotNull().NotEmpty();
        RuleFor(command => command.Password).NotNull().NotEmpty();
    }
}
