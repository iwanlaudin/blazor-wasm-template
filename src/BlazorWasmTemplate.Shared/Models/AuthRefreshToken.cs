namespace BlazorWasmTemplate.Shared.Models;

public class AuthRefreshToken
{
    public AuthRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
    public string? RefreshToken { get; set; }
}

