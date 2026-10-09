using ComputerUI.Models;

namespace ComputerUI.Services;

public interface IComputerApiClient
{
    bool IsLoggedIn { get; }
    Task<bool> LoginAsync(string email, string password);
    void Logout();
    Task<List<Computer>?> GetComputersAsync();
}