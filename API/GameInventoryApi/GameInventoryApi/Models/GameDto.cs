using System.ComponentModel.DataAnnotations;

namespace GameInventoryApi.Models;

// DTO - Data Transfer Object

public class GameDto
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = "";
    [Required]
    public string Genre { get; set; } = "";
    [Range(0, int.MaxValue)] 
    public int HoursPlayed { get; set; }
    public bool Installed { get; set; }
}