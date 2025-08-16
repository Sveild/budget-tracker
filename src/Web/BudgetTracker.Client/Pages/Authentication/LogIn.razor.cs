using System.Threading.Tasks;
using BudgetTracker.Client.Constants;
using BudgetTracker.Client.HttpClients;
using BudgetTracker.Client.Interfaces;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BudgetTracker.Client.Pages.Authentication;

public partial class LogIn
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = null!;

    [Parameter]
    [SupplyParameterFromQuery]
    public string? ReturnUrl { get; set; }

    private bool _displayError = false;

    private async Task LogInAsync(LoginArgs target)
    {
        try
        {
            await AuthenticationService.Login(
                new LogInCommand { UsernameOrEmail = target.Username, Password = target.Password }
            );

            NavigationManager.NavigateTo(!string.IsNullOrEmpty(this.ReturnUrl) ? ReturnUrl : AppRoutes.Home);
        }
        catch (HttpException ex)
        {
            if (ex.StatusCode == 401)
            {
                _displayError = true;
                StateHasChanged();
            }
        }
    }

    private void RegisterAsync()
    {
        NavigationManager.NavigateTo(AppRoutes.Authentication.Register);
    }
}
