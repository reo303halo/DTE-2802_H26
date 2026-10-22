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
}