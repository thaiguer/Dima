using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvider (IHttpClientFactory httpClientFactory):
    AuthenticationStateProvider,
    ICookieAuthenticationStateProvider
{
    private bool _isAuthenticated = false;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<bool> CheckIsAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
        
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        var userInfo = await GetUserAsync();
        if (userInfo is null) return new AuthenticationState(user);

        var claims = await GetUserClaimsAsync(userInfo);
        var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
        user = new ClaimsPrincipal(id);

        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    private async Task<User> GetUserAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<User?>("v1/identity/manage/info");
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetUserClaimsAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(user.Claims.Where(x =>
            x.Key != ClaimTypes.Name &&
            x.Key != ClaimTypes.Email)
            .Select(x => new Claim(x.Key, x.Value))
        );

        RoleClaim[]? roles;
        try
        {
            roles = await _httpClient.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            foreach (var role in roles ?? [])
            {
                if (!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
                {
                    claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
                }
            }
        }
        catch
        {
        }

        return claims;
    }

    
}