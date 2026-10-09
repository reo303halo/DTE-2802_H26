using Bunit;
using ComputerUI.Components.Pages;
using ComputerUI.Models;
using ComputerUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ComputerUI.Tests;

public class ComputersTests : BunitContext
{
    private readonly Mock<IComputerApiClient> _api = new();

    public ComputersTests()
    {
        _api.Setup(a => a.IsLoggedIn).Returns(true); // default: logged in
        Services.AddSingleton(_api.Object);
    }
    
    private static List<Computer> SampleData() =>
    [
        new()
        {
            Model = "ThinkPad X1", Processor = "Intel i7", RamGb = 16, StorageGb = 512,
            Brand = new Brand { Name = "Lenovo", Country = "China" },
            Os = new Os { Name = "Windows", Version = "11" }
        },
        new()
        {
            Model = "MacBook Air", Processor = "Apple M3", RamGb = 8, StorageGb = 256,
            Brand = new Brand { Name = "Apple", Country = "USA" },
            Os = new Os { Name = "macOS", Version = "15" }
        }
    ];

    private string CurrentUrl => Services.GetRequiredService<NavigationManager>().Uri;

    [Fact]
    public void Redirects_to_login_when_not_logged_in()
    {
        _api.Setup(a => a.IsLoggedIn).Returns(false);

        Render<Computers>();
        
        Assert.Equal("http://localhost/login", CurrentUrl);
        _api.Verify(a => a.GetComputersAsync(), Times.Never); // API never called
    }
    
    [Fact]
    public void Shows_loading_while_waiting_for_api()
    {
        var pending = new TaskCompletionSource<List<Computer>?>();
        _api.Setup(a => a.GetComputersAsync()).Returns(pending.Task);

        var cut = Render<Computers>();

        cut.Find("p").MarkupMatches("<p>Loading...</p>");
        Assert.Empty(cut.FindAll("table"));
    }

    [Fact]
    public void Renders_one_row_per_computer()
    {
        _api.Setup(a => a.GetComputersAsync()).ReturnsAsync(SampleData());

        var cut = Render<Computers>();

        var rows = cut.FindAll("tbody tr");
        Assert.Equal(2, rows.Count);

        var firstRow = rows[0].QuerySelectorAll("td");
        Assert.Equal("ThinkPad X1", firstRow[0].TextContent);
        Assert.Equal("Lenovo (China)", firstRow[1].TextContent);
        Assert.Equal("16 GB", firstRow[3].TextContent);
        Assert.Equal("Windows 11", firstRow[5].TextContent);
    }

    [Fact]
    public async Task Replaces_loading_with_table_when_data_arrives()
    {
        var pending = new TaskCompletionSource<List<Computer>?>();
        _api.Setup(a => a.GetComputersAsync()).Returns(pending.Task);

        var cut = Render<Computers>();
        Assert.Contains("Loading...", cut.Markup);

        await cut.InvokeAsync(() => pending.SetResult(SampleData()));

        await cut.WaitForAssertionAsync(() => Assert.Equal(2, cut.FindAll("tbody tr").Count));
        Assert.DoesNotContain("Loading...", cut.Markup);
    }

    [Fact]
    public void Redirects_to_login_when_token_is_rejected()
    {
        _api.Setup(a => a.GetComputersAsync()).ReturnsAsync((List<Computer>?)null);

        Render<Computers>();

        Assert.Equal("http://localhost/login", CurrentUrl);
    }
}