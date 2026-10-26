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
    
    public Cupcake? GetById(int id)
    {
        return db.Cupcakes
            .Include(c => c.Size)
            .Include(c => c.Category)
            .Include(c => c.Owner)
            .FirstOrDefault(c => c.Id == id);
    }
    
    public void Save(Cupcake cupcake, IPrincipal principal)
    {
        var user = userManager.FindByNameAsync(principal.Identity.Name).Result;
        
        cupcake.Owner = user;
        db.Cupcakes.Add(cupcake);
        db.SaveChanges();
    }
    
    public bool Update(Cupcake cupcake, IPrincipal principal)
    {
        // Load the tracked entity and copy fields over, so Owner is not overwritten
        var existing = db.Cupcakes
            .Include(c => c.Owner)
            .FirstOrDefault(c => c.Id == cupcake.Id);

        if (existing is null || !IsOwner(existing, principal)) return false;

        existing.Name = cupcake.Name;
        existing.Description = cupcake.Description;
        existing.SizeId = cupcake.SizeId;
        existing.CategoryId = cupcake.CategoryId;

        db.SaveChanges();
        return true;
    }

    public bool Delete(int id, IPrincipal principal)
    {
        var existing = db.Cupcakes
            .Include(c => c.Owner)
            .FirstOrDefault(c => c.Id == id);

        if (existing is null || !IsOwner(existing, principal)) return false;

        db.Cupcakes.Remove(existing);
        db.SaveChanges();
        return true;
    }

    private static bool IsOwner(Cupcake cupcake, IPrincipal principal) =>
        cupcake.Owner?.UserName == principal.Identity?.Name;
    
    public CupcakeEditViewModel GetCupcakeEditViewModel()
    {
        _viewModel = new CupcakeEditViewModel
        {
            Sizes = [.. db.Sizes],
            Categories = [.. db.Categories]
        };
        return _viewModel;
    }
    
    public CupcakeEditViewModel? GetCupcakeEditViewModel(int id)
    {
        var cupcake = db.Cupcakes.Find(id);
        if (cupcake is null) return null;

        var viewModel = GetCupcakeEditViewModel();
        viewModel.Id = cupcake.Id;
        viewModel.Name = cupcake.Name;
        viewModel.Description = cupcake.Description;
        viewModel.SizeId = cupcake.SizeId;
        viewModel.CategoryId = cupcake.CategoryId;
        return viewModel;
    }
}