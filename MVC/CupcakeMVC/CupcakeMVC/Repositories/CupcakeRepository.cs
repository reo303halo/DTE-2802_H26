using System.Security.Principal;
using CupcakeMVC.Data;
using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CupcakeMVC.Repositories;

public class CupcakeRepository(CupcakeDbContext db, UserManager<IdentityUser> userManager) : ICupcakeRepository
{
    private CupcakeEditViewModel _viewModel;

    public IEnumerable<Cupcake> GetAll()
    {
        try
        {
            var cupcakes = db.Cupcakes
                .Include(c => c.Size)
                .Include(c => c.Category)
                .Include(c => c.Owner)
                .ToList();
            return cupcakes;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);

            return new List<Cupcake>();
        }
    }

    public void Save(Cupcake cupcake, IPrincipal principal)
    {
        var user = userManager.FindByNameAsync(principal.Identity.Name).Result;
        
        cupcake.Owner = user;
        db.Cupcakes.Add(cupcake);
        db.SaveChanges();
    }

    public CupcakeEditViewModel GetCupcakeEditViewModel()
    {
        _viewModel = new CupcakeEditViewModel
        {
            Sizes = [.. db.Sizes],
            Categories = [.. db.Categories]
        };
        return _viewModel;
    }
}