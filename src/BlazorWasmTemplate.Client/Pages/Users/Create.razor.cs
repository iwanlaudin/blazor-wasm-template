using Blazored.Toast.Services;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWasmTemplate.Client.Pages.Users;

public partial class Create
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

private User User { get; set; } = new User();

    private List<RoleVm> RolesVm { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            RolesVm = await UserService.GetAllUserRolesAsync();
        }
        catch (HttpRequestException ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleUserCreate()
    {
        try
        {
            await UserService.PostUserAsync(User);
            NavigationManager.NavigateTo("/users");
        }
        catch (HttpRequestException ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private void HandleButtonCancel()
    {
        NavigationManager.NavigateTo("/users");
    }
}