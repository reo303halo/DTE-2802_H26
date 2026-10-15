using System.ComponentModel.DataAnnotations;

namespace DiceApp.Models;

public class DieSelection
{
    public int Sides { get; set; }
    [Range(0, 20, ErrorMessage = "Roll must be between 0 and 20 for each die.")]
    public int Count { get; set; }
}