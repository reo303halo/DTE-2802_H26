namespace ProductInventory.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "New Category";
    public string? Description { get; set; }
    public List<Product> Products { get; set; } = [];
}