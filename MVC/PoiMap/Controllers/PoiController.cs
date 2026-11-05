using Microsoft.AspNetCore.Mvc;
using PoiMap.Models;
using PoiMap.Repositories;
using PoiMap.ViewModels;

namespace PoiMap.Controllers;

public class PoiController(IPoiRepository repository) : Controller
{
    // GET / - the map page. The markers are fetched by JavaScript from Markers().
    public IActionResult Index() => View();

    // GET /Poi/Markers - POI data as JSON for the map.
    [HttpGet]
    public async Task<IActionResult> Markers()
    {
        var pois = await repository.GetAllAsync();
        return Json(pois.Select(p => p.ToViewModel()));
    }

    // GET /Poi/List
    public async Task<IActionResult> List()
    {
        var pois = await repository.GetAllAsync();
        return View(pois.Select(p => p.ToViewModel()).ToList());
    }

    // GET /Poi/Create
    public IActionResult Create() => View(new PoiFormViewModel());

    // POST /Poi/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PoiFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var poi = new PointOfInterest();
        model.ApplyTo(poi);
        await repository.AddAsync(poi);

        TempData["Message"] = $"\"{poi.Name}\" was created.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Poi/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var poi = await repository.GetByIdAsync(id);
        if (poi is null)
        {
            return NotFound();
        }

        return View(poi.ToFormViewModel());
    }

    // POST /Poi/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PoiFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var poi = await repository.GetByIdAsync(id);
        if (poi is null)
        {
            return NotFound();
        }

        model.ApplyTo(poi);
        await repository.UpdateAsync(poi);

        TempData["Message"] = $"\"{poi.Name}\" was updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Poi/Delete/5 - confirmation page.
    public async Task<IActionResult> Delete(int id)
    {
        var poi = await repository.GetByIdAsync(id);
        if (poi is null)
        {
            return NotFound();
        }

        return View(poi.ToViewModel());
    }

    // POST /Poi/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var poi = await repository.GetByIdAsync(id);
        if (poi is null)
        {
            return NotFound();
        }

        await repository.DeleteAsync(poi);

        TempData["Message"] = $"\"{poi.Name}\" was deleted.";
        return RedirectToAction(nameof(Index));
    }
}
