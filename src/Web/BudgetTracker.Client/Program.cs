using System;
using System.Linq;
using System.Net.Http;
using Blazored.LocalStorage;
using BudgetTracker.Client;
using BudgetTracker.Client.HttpClients;
using BudgetTracker.Client.Interfaces;
using BudgetTracker.Client.Providers;
using BudgetTracker.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// automatically add all http clients to the service container
var httpClients = typeof(BaseHttpClient)
    .Assembly.GetTypes()
    .Where(type => type.IsSubclassOf(typeof(BaseHttpClient)))
    .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition)
    .Where(type => type.GetInterfaces().Length == 1)
    .Select(type => new { Service = type.GetInterfaces().Single(), Implementation = type });

foreach (var httpClient in httpClients)
{
    builder.Services.AddScoped(httpClient.Service, httpClient.Implementation);
}

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
