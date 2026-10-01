namespace ComputerUI.Models;

public class Computer
{
    public int Id { get; set; }
    public string Model { get; set; } = "";
    public string Processor { get; set; } = "";
    public int RamGb { get; set; }
    public int StorageGb { get; set; }
    public Brand Brand { get; set; } = new();
    public Os Os { get; set; } = new();
}