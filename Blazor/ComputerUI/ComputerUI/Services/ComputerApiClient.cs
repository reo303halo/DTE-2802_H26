using System.Net;
using System.Net.Http.Headers;
using ComputerUI.Models;

namespace ComputerUI.Services;

public record LoginResponse(string AccessToken);

public class ComputerApiClient(HttpClient http)
{
    private string? _token;

    public bool IsLoggedIn => _token != null;

    public async Task<bool> LoginAsync(string email, string password)
    {
        var res = await http.PostAsJsonAsync("login", new { email, password });
        if (!res.IsSuccessStatusCode) return false;
        
        var body = await res.Content.ReadFromJsonAsync<LoginResponse>();
        _token = body?.AccessToken;
        return _token != null;
    }
    
    public void Logout() => _token = null;

    public Task<List<Computer>?> GetComputersAsync() => GetAsync<List<Computer>>("Computer");

    private async Task<T?> GetAsync<T>(string url)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        using var res = await http.SendAsync(req);
        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            _token = null; // expired or invalid
            return default;
        }

        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }
}