using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using BlazorWasmTemplate.Application.Services;
using BlazorWasmTemplate.Application.Comman;
using BlazorWasmTemplate.Application.Interfaces;

namespace BlazorWasmTemplate.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ApplicationSettings applicationSettings)
    {
        // blazored services
        services.AddBlazoredToast();
        services.AddBlazoredLocalStorage();

        // authetication & authorization
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddTransient<CustomHttpClientHandler>();

        // configuring http clients
        services.AddHttpClient("komodoWaterApiV1", client =>
        {
            client.BaseAddress = new Uri(applicationSettings.BaseAddress);
        }).AddHttpMessageHandler<CustomHttpClientHandler>();

        // use case services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        // js runtime service
        services.AddScoped<IJSRuntimeService, JSRuntimeService>();

        return services;
    }
}

