using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInventory.Models.Entities;

public class Product
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = "New Product";
    [MaxLength(255)]
    public string? Description { get; set; }
    
    [Column(TypeName = "decimal(8,2)")]
    public decimal Price { get; set; }
    
    // Navigational Properties
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}