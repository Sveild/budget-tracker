using FluentValidation;

namespace BudgetTracker.Application.Authentication.LogIn;

public class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(command => command.UsernameOrEmail).NotNull().NotEmpty();
        RuleFor(command => command.Password).NotNull().NotEmpty();
    }
}
