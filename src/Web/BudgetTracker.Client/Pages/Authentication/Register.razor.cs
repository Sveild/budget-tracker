using System.Threading.Tasks;
using BudgetTracker.Client.Constants;
using BudgetTracker.Client.HttpClients;
using BudgetTracker.Client.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BudgetTracker.Client.Pages.Authentication;

public partial class Register : ComponentBase
{
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private RegisterCommand _registerCommand = new();

    private async Task Submit(RegisterCommand command)
    {
        var result = await AuthenticationService.Register(command);

        if (result.Succeeded)
        {
            NavigationManager.NavigateTo(AppRoutes.Authentication.Login);
        }
    }
}
