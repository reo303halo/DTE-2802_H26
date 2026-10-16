using System.ComponentModel.DataAnnotations;
using ProductInventory.Models.Entities;

namespace ProductInventory.Models.ViewModels;

public class ProductEditViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Product Name is required")]
    public string Name { get; set; } = "New Product";
    [MaxLength(255)]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Product price is required")]
    public decimal Price { get; set; } = 0.0m;
    public int ManufacturerId { get; set; }
    public List<Manufacturer> Manufacturers { get; set; } = [];
    public int CategoryId { get; set; }
    public List<Category> Categories { get; set; } = [];
}