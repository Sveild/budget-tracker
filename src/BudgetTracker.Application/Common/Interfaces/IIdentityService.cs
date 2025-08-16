using System;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Models;

namespace BudgetTracker.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> IsInRoleAsync(Guid userId, string role);

    Task<bool> AuthorizeAsync(Guid userId, string policyName);

    Task<string> SignInAsync(string usernameOrEmail, string password);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string email, string password);

    Task<Result> DeleteUserAsync(Guid userId);
}
