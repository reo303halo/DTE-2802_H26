using System.Security.Principal;
using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;

namespace CupcakeMVC.Repositories;

public interface ICupcakeRepository
{
    IEnumerable<Cupcake> GetAll();
    Cupcake? GetById(int id);
    void Save(Cupcake cupcake, IPrincipal principal);
    bool Update(Cupcake cupcake, IPrincipal principal);
    bool Delete(int id, IPrincipal principal);
    CupcakeEditViewModel GetCupcakeEditViewModel();
    CupcakeEditViewModel? GetCupcakeEditViewModel(int id);
}