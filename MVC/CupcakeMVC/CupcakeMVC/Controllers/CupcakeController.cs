using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;
using CupcakeMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeMVC.Controllers;

public class CupcakeController(ICupcakeRepository repository) : Controller
{
    public IActionResult Index()
    {
        var cupcakes = repository.GetAll();
        return View(cupcakes);
    }
    
    // Create : GET
    [Authorize]
    public IActionResult Create()
    {
        var cupcake = repository.GetCupcakeEditViewModel();
        return View(cupcake);
    }
    
    // Create : POST
    [Authorize]
    [HttpPost]
    public IActionResult Create([Bind("Name,Description,SizeId,CategoryId")] CupcakeEditViewModel cupcake)
    {
        try
        {
            if (!ModelState.IsValid) return View();

            var c = new Cupcake
            {
                Id = cupcake.Id,
                Name = cupcake.Name,
                Description = cupcake.Description,
                SizeId = cupcake.SizeId,
                CategoryId = cupcake.CategoryId
            };
            
            repository.Save(c, User);
            TempData["Message"] = $"Cupcake with name '{c.Name}' created successfully";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["Message"] = "Obs something went wrong";
            return RedirectToAction("Index");
        }
    }
    
    // Edit : GET
    [Authorize]
    public IActionResult Edit(int id)
    {
        var cupcake = repository.GetById(id);
        if (cupcake is null) return NotFound();
        if (cupcake.Owner?.UserName != User.Identity?.Name) return Forbid();

        return View(repository.GetCupcakeEditViewModel(id));
    }

    // Edit : POST
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name,Description,SizeId,CategoryId")] CupcakeEditViewModel cupcake)
    {
        if (id != cupcake.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            // Repopulate the dropdowns, otherwise the view crashes
            var lists = repository.GetCupcakeEditViewModel();
            cupcake.Sizes = lists.Sizes;
            cupcake.Categories = lists.Categories;
            return View(cupcake);
        }

        var c = new Cupcake
        {
            Id = cupcake.Id,
            Name = cupcake.Name,
            Description = cupcake.Description,
            SizeId = cupcake.SizeId,
            CategoryId = cupcake.CategoryId
        };

        if (!repository.Update(c, User)) return Forbid();

        TempData["Message"] = $"Cupcake '{c.Name}' updated successfully";
        return RedirectToAction("Index");
    }

    // Delete : GET (confirmation page)
    [Authorize]
    public IActionResult Delete(int id)
    {
        var cupcake = repository.GetById(id);
        if (cupcake is null) return NotFound();
        if (cupcake.Owner?.UserName != User.Identity?.Name) return Forbid();

        return View(cupcake);
    }

    // Delete : POST
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!repository.Delete(id, User)) return Forbid();

        TempData["Message"] = "Cupcake deleted";
        return RedirectToAction("Index");
    }
}