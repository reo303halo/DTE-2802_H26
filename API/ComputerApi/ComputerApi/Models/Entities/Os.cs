namespace ComputerApi.Models.Entities;

public class Os
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Version { get; set; } = "";
    
    public ICollection<Computer> Computers { get; set; } = new List<Computer>();
}