using Blazored.Toast.Services;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmTemplate.Client.Pages.Users;

public partial class Detail
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

[Parameter] public Guid? UserId { get; set; }

    public UserVm? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserId.HasValue)
        {
            try
            {
                User = await UserService.GetUserByIdAsync((Guid)UserId);
            }
            catch (HttpRequestException ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }
        else
        {
            NavigationManager.NavigateTo("/users");
        }
    }

    private void HandleClick()
    {
        Console.WriteLine("Add User button clicked!");
    }
}