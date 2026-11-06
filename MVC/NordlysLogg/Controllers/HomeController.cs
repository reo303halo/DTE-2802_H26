using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NordlysLogg.Models;
using NordlysLogg.Repositories;
using NordlysLogg.ViewModels;

namespace NordlysLogg.Controllers;

public class HomeController(
    ISightingRepository sightings,
    ISiteRepository sites,
    IObserverRepository observers) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalSightings = await sightings.CountAsync(),
            SiteCount = await sites.CountAsync(),
            ObserverCount = await observers.CountAsync(),
            MostSpectacular = await sightings.GetMostSpectacularAsync(),
            Recent = await sightings.GetRecentAsync(4)
        };

        return View(model);
    }

    public IActionResult About() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
