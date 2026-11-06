using Microsoft.AspNetCore.Mvc;
using NordlysLogg.Models;
using NordlysLogg.Repositories;
using NordlysLogg.ViewModels;

namespace NordlysLogg.Controllers;

public class SitesController(ISiteRepository sites) : Controller
{
    // GET: /Sites
    public async Task<IActionResult> Index()
    {
        var model = (await sites.GetAllAsync())
            .Select(SiteListItemViewModel.FromEntity)
            .ToList();
        return View(model);
    }

    // GET: /Sites/Details/3
    public async Task<IActionResult> Details(int id)
    {
        var site = await sites.GetByIdWithSightingsAsync(id);
        return site is null ? NotFound() : View(SiteDetailsViewModel.Create(site));
    }

    // GET: /Sites/Create
    public IActionResult Create() => View(new ObservationSite());

    // POST: /Sites/Create
    // Kontrast til SightingsController: her bindes entiteten direkte, men [Bind]
    // begrenser hvilke felter som tas imot. Diskuter: hva er fordelene med en ViewModel i stedet?
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(nameof(ObservationSite.Name), nameof(ObservationSite.Region),
              nameof(ObservationSite.Latitude), nameof(ObservationSite.Longitude),
              nameof(ObservationSite.DarknessRating), nameof(ObservationSite.Description))]
        ObservationSite site)
    {
        if (!ModelState.IsValid) return View(site);

        await sites.AddAsync(site);
        TempData["Message"] = $"{site.Name} er lagt til som observasjonssted.";
        return RedirectToAction(nameof(Details), new { id = site.Id });
    }
}
