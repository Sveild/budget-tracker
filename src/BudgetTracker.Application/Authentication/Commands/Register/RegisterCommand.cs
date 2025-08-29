using BudgetTracker.Application.Common.Models;
using MediatR;

namespace BudgetTracker.Application.Authentication.Commands.Register;

public record RegisterCommand : IRequest<Result>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
