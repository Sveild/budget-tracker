using System.Threading.Tasks;
using BudgetTracker.Client.HttpClients;

namespace BudgetTracker.Client.Interfaces;

public interface IAuthenticationService
{
    Task<Result> Register(RegisterCommand command);
    Task<LogInResult> Login(LogInCommand command);
    Task Logout();
}
