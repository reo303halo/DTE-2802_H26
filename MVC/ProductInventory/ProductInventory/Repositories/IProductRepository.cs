using ProductInventory.Models.Entities;
using ProductInventory.Models.ViewModels;

namespace ProductInventory.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    void Save(Product product);
    void Delete(int id);
    ProductEditViewModel GetProductEditViewModel();
    ProductEditViewModel GetProductEditViewModel(int id);
}