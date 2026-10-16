using Microsoft.AspNetCore.Mvc;
using ProductInventory.Models.Entities;
using ProductInventory.Models.ViewModels;
using ProductInventory.Repositories;

namespace ProductInventory.Controllers;

public class ProductController(IProductRepository repository): Controller 
{
    // GET 
    public IActionResult Index()
    {
        var products = repository.GetProducts();
        return View(products);
    }
    
    // GET
    public IActionResult Create()
    {
        var product = repository.GetProductEditViewModel();
        return View(product);
    }
    
    // POST
    [HttpPost]
    public IActionResult Create([Bind("Name,Description,Price,ManufacturerId,CategoryId")] ProductEditViewModel product)
    {
        try
        {
            if (!ModelState.IsValid) return View();
            var p = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ManufacturerId = product.ManufacturerId,
                CategoryId = product.CategoryId
            };
            
            repository.Save(p);
            TempData["message"] = $"{product.Name} has been created successfully";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View();
        }
    }
    
    // GET
    public IActionResult Edit(int id)
    {
        var product = repository.GetProductEditViewModel(id);
        return View(product);
    }
    
    // POST
    [HttpPost]
    public IActionResult Edit(int id, [Bind("ProductId,Name,Description,Price,ManufacturerId,CategoryId")] ProductEditViewModel product)
    {
        if (!ModelState.IsValid) return View(product);
        try
        {
            var p = new Product
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ManufacturerId = product.ManufacturerId,
                CategoryId = product.CategoryId
            };
            
            repository.Save(p);
            TempData["message"] = $"{product.Name} has been updated!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View();
        }
    }
    
    // GET / POST
    public IActionResult Delete(int id)
    {
        try
        {
            var product = repository.GetProductEditViewModel(id);
            repository.Delete(product.Id);
            TempData["message"] = $"{product.Name} has been deleted successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["message"] = "No product deleted!";
        }
        return RedirectToAction("Index");
    }
}