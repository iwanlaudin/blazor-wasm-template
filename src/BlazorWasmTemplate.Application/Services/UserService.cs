using BlazorWasmTemplate.Application.Constants;
using BlazorWasmTemplate.Application.Extensions;
using BlazorWasmTemplate.Application.Interfaces;
using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Shared.Wrapper;
using System.Net.Http.Json;
using System.Text;

namespace BlazorWasmTemplate.Application.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClient;

    public UserService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PagedList<UserVm>> GetAllUserAsync(int? page, string? search)
    {
        StringBuilder queries = new();
        if (page.HasValue)
        {
            queries.Append($"&page={page}");
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            queries.Append($"&search={search}");
        }

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllUsers}?{queries}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<PagedList<UserVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<UserVm> GetUserByIdAsync(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetUserById}{userId}");
        await response.HandleErrorResponseAsync();

        UserVm? result = await response.Content.ReadFromJsonAsync<UserVm>();
        return await Task.FromResult(result!);
    }

    public async Task<List<RoleVm>> GetAllUserRolesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllUserRoles}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<RoleVm>>();
        return await Task.FromResult(result!);
    }

    public async Task PostUserAsync(User user)
    {
        HttpResponseMessage response = await _httpClient.PostAsync($"{ApiEndpoint.CreateUser}", user);
        await response.HandleErrorResponseAsync();
    }

    public async Task PutUserAsync(Guid userId, User user)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"{ApiEndpoint.UpdateUser}{userId}", user);
        await response.HandleErrorResponseAsync();
    }
}

