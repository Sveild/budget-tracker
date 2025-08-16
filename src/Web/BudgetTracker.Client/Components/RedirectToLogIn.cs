using System.Web;
using BudgetTracker.Client.Constants;
using Microsoft.AspNetCore.Components;

namespace BudgetTracker.Client.Components;

public class RedirectToLogIn : ComponentBase
{
    [Inject]
    private NavigationManager _navigationManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        var returnUrl = HttpUtility.UrlEncode(_navigationManager.ToBaseRelativePath(_navigationManager.Uri));

        _navigationManager.NavigateTo(
            $"{AppRoutes.Authentication.Login}{(string.IsNullOrEmpty(returnUrl) ? "" : $"?returnUrl={returnUrl}")}",
            forceLoad: true
        );
    }
}
