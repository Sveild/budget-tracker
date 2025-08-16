using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BudgetTracker.Client.HttpClients;
using BudgetTracker.Client.Interfaces;
using BudgetTracker.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BudgetTracker.Client.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticationClient _authenticationClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(
        HttpClient httpClient,
        IAuthenticationClient authenticationClient,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage
    )
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
        _authenticationClient = authenticationClient;
    }

    public async Task<Result> Register(RegisterCommand command)
    {
        try
        {
            return await _authenticationClient.RegisterAsync(command);
        }
        catch (HttpException ex)
        {
            return ex.StatusCode != 400 || string.IsNullOrEmpty(ex.Response)
                ? new Result { Succeeded = false }
                : Result.FromJson(ex.Response);
        }
    }

    public async Task<LogInResult> Login(LogInCommand command)
    {
        var loginResult = await _authenticationClient.LogInAsync(command);

        await _localStorage.SetItemAsync("authToken", loginResult.Token);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(command.UsernameOrEmail);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
