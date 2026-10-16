namespace ProductInventory.Models.Entities;

public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = "New Manufacturer";
    public string? Description { get; set; }
    public string? Address { get; set; }
    public List<Product> Products { get; set; } = [];
}