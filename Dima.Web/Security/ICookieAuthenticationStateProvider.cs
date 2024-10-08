using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckIsAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}