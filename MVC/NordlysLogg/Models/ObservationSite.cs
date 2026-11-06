using System.ComponentModel.DataAnnotations;

namespace NordlysLogg.Models;

/// <summary>Et sted hvor man kan se nordlys.</summary>
public class ObservationSite
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    [Display(Name = "Stedsnavn")]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(60)]
    [Display(Name = "Region")]
    public string Region { get; set; } = string.Empty;

    [Range(-90, 90)]
    [Display(Name = "Breddegrad")]
    public double Latitude { get; set; }

    [Range(-180, 180)]
    [Display(Name = "Lengdegrad")]
    public double Longitude { get; set; }

    [Range(1, 5)]
    [Display(Name = "Mørkhet (1–5)")]
    public int DarknessRating { get; set; } = 3;

    [StringLength(500)]
    [Display(Name = "Beskrivelse")]
    public string? Description { get; set; }

    public ICollection<AuroraSighting> Sightings { get; set; } = new List<AuroraSighting>();
}
