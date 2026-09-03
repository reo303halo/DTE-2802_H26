namespace ComputerApi.Models;

public class ComputerDto
{
    public int Id { get; set; }

    public string Model { get; set; } = "";
    public string Processor { get; set; } = "";
    
    public int RamGb { get; set; }
    public int StorageGb { get; set; }
    
    public BrandDto Brand { get; set; } = null!;
    public OsDto Os { get; set; } = null!;
}