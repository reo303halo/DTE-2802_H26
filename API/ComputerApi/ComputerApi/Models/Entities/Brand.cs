namespace ComputerApi.Models.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Country { get; set; } = "";
    
    public ICollection<Computer> Computers { get; set; } = new List<Computer>();
} 