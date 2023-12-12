using Blazored.LocalStorage;
using BlazorWasmTemplate.Application.Comman;
using BlazorWasmTemplate.Application.Constants;
using BlazorWasmTemplate.Application.Extensions;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Wrapper;
using System.Text.Json;

namespace BlazorWasmTemplate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public AuthService(
        IHttpClientFactory httpClient,
        AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
    }

    public async Task SignInAsync(Authentication request)
    {
        var response = await _httpClient.PostAsync($"{ApiEndpoint.SignIn}", request);
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Error err = JsonSerializer.Deserialize<Error>(responseBody)!;
            throw new Exception(err.Message);
        }

        var result = JsonSerializer.Deserialize<AuthenticationVm>(responseBody)!;
        await _localStorageService.SetItemAsStringAsync("AUTH_TOKEN", result!.AccessToken);
        await _localStorageService.SetItemAsStringAsync("REFRESH_TOKEN", result!.RefreshToken);

        ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(result!.AccessToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task SignOut()
    {
        await _localStorageService.RemoveItemAsync("AUTH_TOKEN");
        await _localStorageService.RemoveItemAsync("REFRESH_TOKEN");
        ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
    }
}

