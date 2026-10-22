using System.Security.Principal;
using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;

namespace CupcakeMVC.Repositories;

public interface ICupcakeRepository
{
    IEnumerable<Cupcake> GetAll();
    void Save(Cupcake cupcake, IPrincipal principal);
    CupcakeEditViewModel GetCupcakeEditViewModel();
}