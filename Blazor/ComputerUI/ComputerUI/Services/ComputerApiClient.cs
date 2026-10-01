using ComputerUI.Models;

namespace ComputerUI.Services;

public class ComputerApiClient(HttpClient http)
{
    public Task<List<Computer>?> GetComputersAsync() =>
        http.GetFromJsonAsync<List<Computer>?>("Computer");
}