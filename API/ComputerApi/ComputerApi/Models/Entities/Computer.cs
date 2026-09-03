namespace ComputerApi.Models.Entities;

public class Computer
{
    public int Id { get; set; }

    public string Model { get; set; } = "";
    public string Processor { get; set; } = "";
    
    public int RamGb { get; set; }
    public int StorageGb { get; set; }
    
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    
    public int OsId { get; set; }
    public Os Os { get; set; } = null!;
}