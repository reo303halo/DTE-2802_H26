namespace ComputerApi.Models;

public class CreateComputerDto
{
    public required string Model { get; set; }
    public required string Processor { get; set; }
    public int RamGb { get; set; }
    public int StorageGb { get; set; }
    public int BrandId { get; set; }
    public int OsId { get; set; }
}