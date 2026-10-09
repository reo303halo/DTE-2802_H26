using System.Net;
using System.Net.Http.Headers;
using ComputerUI.Models;

namespace ComputerUI.Services;

public record LoginResponse(string AccessToken);

public class ComputerApiClient(HttpClient http, TokenStore tokens): IComputerApiClient
{
    public bool IsLoggedIn => tokens.Token is not null;

    public async Task<bool> LoginAsync(string email, string password)
    {
        var res = await http.PostAsJsonAsync("login", new { email, password });
        if (!res.IsSuccessStatusCode) return false;
        
        var body = await res.Content.ReadFromJsonAsync<LoginResponse>();
        tokens.Token = body?.AccessToken;
        return IsLoggedIn;
    }
    
    public void Logout() => tokens.Token = null;

    public Task<List<Computer>?> GetComputersAsync() => GetAsync<List<Computer>>("Computer");

    private async Task<T?> GetAsync<T>(string url)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.Token);

        using var res = await http.SendAsync(req);
        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            tokens.Token = null; // expired or invalid
            return default;
        }

        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }
}