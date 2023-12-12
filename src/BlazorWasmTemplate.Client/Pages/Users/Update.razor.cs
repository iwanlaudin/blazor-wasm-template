using Blazored.Toast.Services;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWasmTemplate.Client.Pages.Users;

public partial class Update
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [Parameter]
    public Guid? UserId { get; set; }

    private User User { get; set; } = new();

    public UserVm UserVm { get; set; } = new();

    public List<RoleVm> RolesVm { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (UserId.HasValue)
            {
                UserVm = await UserService.GetUserByIdAsync((Guid)UserId);
                RolesVm = await UserService.GetAllUserRolesAsync();
                User = new User
                {
                    Username = UserVm.Username,
                    FullName = UserVm.FullName,
                    Email = UserVm.Email,
                    PhoneNumber = UserVm.PhoneNumber,
                    RoleId = UserVm.RoleId,
                    IsCreateOperation = false
                };
            }
            else
            {
                NavigationManager.NavigateTo("/users");
            }
        }
        catch (HttpRequestException ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleUserUpdate()
    {
        try
        {
            if (UserId.HasValue)
            {
                await UserService.PutUserAsync((Guid)UserId, User);
                NavigationManager.NavigateTo($"/users/detail/{UserId}");
            }
            else
            {
                ToastService.ShowError("Ooops! Terjadi kesalahan.");
            }
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