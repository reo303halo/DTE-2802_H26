using System.ComponentModel.DataAnnotations;

namespace NordlysLogg.Models;

/// <summary>En person som registrerer nordlysobservasjoner.</summary>
public class Observer
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    [Display(Name = "Navn")]
    public string Name { get; set; } = string.Empty;

    [StringLength(40)]
    [Display(Name = "Kallenavn")]
    public string? Nickname { get; set; }

    // Navigasjonsegenskap: én observatør har mange observasjoner (1-til-mange)
    public ICollection<AuroraSighting> Sightings { get; set; } = new List<AuroraSighting>();
}
