using Blazored.LocalStorage;
using BlazorWasmTemplate.Application.Constants;
using BlazorWasmTemplate.Application.Extensions;
using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWasmTemplate.Application.Comman;

public class CustomHttpClientHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthenticationStateProvider _authStateProvider;

    public CustomHttpClientHandler(
        ILocalStorageService localStorageService,
        IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authStateProvider)
    {
        _localStorageService = localStorageService;
        _httpClientFactory = httpClientFactory;
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var excludedPaths = new string[] { "sign-in", "refresh" };
        if (excludedPaths.Any(path => request.RequestUri!.AbsolutePath.ToLower().Contains(path)))
            return await base.SendAsync(request, cancellationToken);
        
        var token = await _localStorageService.GetItemAsStringAsync("AUTH_TOKEN", cancellationToken);
        if (!string.IsNullOrEmpty(token))
            request.Headers.Add("Authorization", $"Bearer {token}");

        var response =  await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return await InvokeRefreshTokenCall(response, request, cancellationToken);

        return response;
    }

    private async Task<HttpResponseMessage> InvokeRefreshTokenCall(
        HttpResponseMessage response,
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var refreshToken = await _localStorageService.GetItemAsStringAsync("REFRESH_TOKEN", cancellationToken);
        if (string.IsNullOrEmpty(refreshToken))
        {
            ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            throw new Exception("Refresh token is not available.");
        }
            
        var newRequest = new AuthRefreshToken(refreshToken);
        var refrehTokenResponse = await _httpClientFactory.PostAsync(ApiEndpoint.Refresh, newRequest);
        if (refrehTokenResponse.StatusCode == HttpStatusCode.OK)
        {
            var newToken = await refrehTokenResponse.Content.ReadFromJsonAsync<AuthenticationVm>(cancellationToken: cancellationToken);
            await _localStorageService.SetItemAsStringAsync("AUTH_TOKEN", newToken!.AccessToken, cancellationToken);
            await _localStorageService.SetItemAsStringAsync("REFRESH_TOKEN", newToken!.RefreshToken, cancellationToken);

            ((CustomAuthenticationStateProvider)_authStateProvider)
                .MarkUserAsAuthenticated(newToken!.AccessToken);

            // Update request with the new token
            request.Headers.Remove("Authorization");
            request.Headers.Add("Authorization", $"Bearer {newToken.AccessToken}");

            // Send the updated request
            return await base.SendAsync(request, cancellationToken);
        }

        return response;
    }
}

