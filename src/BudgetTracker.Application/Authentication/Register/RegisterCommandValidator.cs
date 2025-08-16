using FluentValidation;

namespace BudgetTracker.Application.Authentication.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.Password);
    }
}
