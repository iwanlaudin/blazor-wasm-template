using Blazored.Toast.Services;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmTemplate.Client.Pages.Auth;

public partial class SignIn
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IAuthService AuthService { get; set; } = default!;

    public Authentication Authentication { get; set; } = new();
    public string? error { get; set; } = string.Empty;

    public async Task HandleUserSignin()
    {
        try
        {
            await AuthService.SignInAsync(Authentication);
            NavigationManager.NavigateTo("/");
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
}