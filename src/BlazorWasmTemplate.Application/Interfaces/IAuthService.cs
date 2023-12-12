using BlazorWasmTemplate.Shared.Models;

namespace BlazorWasmTemplate.Application.Interfaces;

public interface IAuthService
{
    Task SignInAsync(Authentication request);
    Task SignOut();
}

