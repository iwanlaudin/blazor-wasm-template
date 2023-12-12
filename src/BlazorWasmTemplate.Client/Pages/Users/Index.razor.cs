using Blazored.Toast.Services;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Shared.Wrapper;

namespace BlazorWasmTemplate.Client.Pages.Users;

public partial class Index
{
    [Inject] IToastService ToastService { get; set; } = default!;
    [Inject] IUserService UserService { get; set; } = default!;

    private int currentPage = 1;
    private readonly int pageSize = 5;
    private string? search;
    private bool isLoading = true;

    public PagedList<UserVm>? users;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        isLoading = false;
    }

    private async Task OnPageChange(int newPage)
    {
        currentPage = newPage;
        await LoadData();

        if (!(users is not null && users.Items!.Count > 0))
        {
            ToastService.ShowWarning("Data is empty on this page.");

            currentPage -= 1;
            await LoadData();
        }

        StateHasChanged();
    }

    private async Task LoadData()
    {
        try
        {
            users = await UserService.GetAllUserAsync(currentPage, search);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task OnSearch(string s)
    {
        search = s;
        currentPage = 1;
        await LoadData();
    }
}