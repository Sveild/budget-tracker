using System;
using Microsoft.AspNetCore.Components;

namespace BudgetTracker.Client.Layout;

public partial class NavMenu
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private void Navigateto(string route)
    {
        NavigationManager.NavigateTo(route);
    }
}
