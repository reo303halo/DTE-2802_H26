namespace DiceApp.Models;

public record DieResult(int Sides, int Value);

public class DiceViewModel
{
    public List<DieSelection> Dice { get; set; } = [];
    public List<DieResult> Results { get; set; } = [];
    
    public int Total => Results.Sum(r => r.Value);
}