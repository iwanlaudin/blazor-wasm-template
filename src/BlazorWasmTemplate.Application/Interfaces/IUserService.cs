using BlazorWasmTemplate.Shared.Models;
using BlazorWasmTemplate.Shared.ViewModels;
using Shared.Wrapper;

namespace BlazorWasmTemplate.Application.Interfaces;

public interface IUserService
{
    Task<PagedList<UserVm>> GetAllUserAsync(int? page, string? search);
    Task<UserVm> GetUserByIdAsync(Guid userId);
    Task PostUserAsync(User user);
    Task PutUserAsync(Guid userId, User user);
    Task<List<RoleVm>> GetAllUserRolesAsync();
}

