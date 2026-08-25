namespace GameInventoryApi.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int HoursPlayed { get; set; }
    public bool Installed { get; set; }
}