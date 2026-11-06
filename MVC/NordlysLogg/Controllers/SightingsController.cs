using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NordlysLogg.Repositories;
using NordlysLogg.ViewModels;

namespace NordlysLogg.Controllers;

/// <summary>Full CRUD for nordlysobservasjoner – via repository og ViewModels.</summary>
public class SightingsController(
    ISightingRepository sightings,
    ISiteRepository sites,
    IObserverRepository observers) : Controller
{
    // GET: /Sightings?siteId=1&minKp=4&sort=kp
    public async Task<IActionResult> Index(int? siteId, int? minKp, string? sort)
    {
        var model = new SightingIndexViewModel
        {
            SiteId = siteId,
            MinKp = minKp,
            Sort = sort ?? "newest",
            Sightings = await sightings.GetAllAsync(siteId, minKp, sort),
            Sites = await SiteSelectListAsync()
        };
        return View(model);
    }

    // GET: /Sightings/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var sighting = await sightings.GetByIdAsync(id);
        return sighting is null ? NotFound() : View(sighting);
    }

    // GET: /Sightings/Create
    public async Task<IActionResult> Create(int? siteId)
    {
        var model = new SightingFormViewModel { SiteId = siteId ?? 0 };
        await FillSelectListsAsync(model);
        return View(model);
    }

    // POST: /Sightings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SightingFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Nedtrekkslistene postes ikke tilbake, så de må fylles på nytt
            await FillSelectListsAsync(model);
            return View(model);
        }

        var entity = model.ToEntity();
        await sightings.AddAsync(entity);

        TempData["Message"] = $"Observasjonen er lagret. Wow-score: {entity.WowScore} ✨";
        return RedirectToAction(nameof(Details), new { id = entity.Id }); // Post/Redirect/Get
    }

    // GET: /Sightings/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var sighting = await sightings.GetByIdAsync(id);
        if (sighting is null) return NotFound();

        var model = SightingFormViewModel.FromEntity(sighting);
        await FillSelectListsAsync(model);
        return View(model);
    }

    // POST: /Sightings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SightingFormViewModel model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await FillSelectListsAsync(model);
            return View(model);
        }

        var sighting = await sightings.GetByIdAsync(id);
        if (sighting is null) return NotFound();

        model.ApplyTo(sighting);          // kopier kun tillatte felter
        await sightings.UpdateAsync(sighting);

        TempData["Message"] = "Endringene er lagret.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: /Sightings/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var sighting = await sightings.GetByIdAsync(id);
        return sighting is null ? NotFound() : View(sighting);
    }

    // POST: /Sightings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await sightings.DeleteAsync(id)) return NotFound();

        TempData["Message"] = "Observasjonen er slettet.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Hjelpemetoder ----------

    private async Task FillSelectListsAsync(SightingFormViewModel model)
    {
        model.Sites = await SiteSelectListAsync();
        model.Observers = (await observers.GetAllAsync())
            .Select(o => new SelectListItem(
                o.Nickname is null ? o.Name : $"{o.Name} ({o.Nickname})",
                o.Id.ToString()));
    }

    private async Task<IEnumerable<SelectListItem>> SiteSelectListAsync() =>
        (await sites.GetAllAsync())
            .Select(s => new SelectListItem(s.Name, s.Id.ToString()));
}
