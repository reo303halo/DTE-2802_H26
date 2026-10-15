using DiceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiceApp.Controllers;

public class DiceController : Controller
{
    private static readonly int[] AllowedDice = [4, 6, 8, 10, 12, 20, 100];
    
    // GET /Dice => show the empty form
    [HttpGet]
    public IActionResult Index()
    {
        var model = new DiceViewModel
        {
            Dice = [.. AllowedDice.Select(s => new DieSelection { Sides = s })]
        };
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(DiceViewModel model)
    {
        model.Dice = [.. model.Dice.Where(d => AllowedDice.Contains(d.Sides))];
        
        if (model.Dice.Sum(d => d.Count) == 0)
            ModelState.AddModelError("", "Choose at least one die to roll");

        if (!ModelState.IsValid)
            return View(model);

        model.Results =
        [
            .. model.Dice
                .SelectMany(d => Enumerable.Range(0, d.Count)
                    .Select(_ => new DieResult(d.Sides, Random.Shared.Next(1, d.Sides + 1))))
        ];
        
        return View(model);
    }

}