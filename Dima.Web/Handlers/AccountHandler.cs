using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login sucessfull", 200, "")
            : new Response<string>("Login error", 400, "");
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Register sucessfull", 201, "")
            : new Response<string>("Register error", 400, "");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
        await _httpClient.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }
}