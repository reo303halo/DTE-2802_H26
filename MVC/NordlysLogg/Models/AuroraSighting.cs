using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NordlysLogg.Models;

/// <summary>Én registrert nordlysobservasjon. Dette er domenemodellen (entiteten) som EF lagrer.</summary>
public class AuroraSighting
{
    public int Id { get; set; }

    public DateTime ObservedAt { get; set; }

    /// <summary>Planetær geomagnetisk indeks, 0–9.</summary>
    [Range(0, 9)]
    public int KpIndex { get; set; }

    public AuroraIntensity Intensity { get; set; }

    public AuroraColor DominantColor { get; set; }

    [Range(0, 100)]
    public int CloudCoverPercent { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    // Fremmednøkler + navigasjonsegenskaper
    public int SiteId { get; set; }
    public ObservationSite? Site { get; set; }

    public int ObserverId { get; set; }
    public Observer? Observer { get; set; }

    /// <summary>
    /// Beregnet "Wow-score" (0–100). [NotMapped] = EF lager ingen kolonne for den.
    /// Fin å bruke i undervisning: logikk som hører til modellen, men ikke til databasen.
    /// </summary>
    [NotMapped]
    public int WowScore =>
        Math.Clamp(KpIndex * 7 + (int)Intensity * 10 + (100 - CloudCoverPercent) / 5 - 3, 0, 100);
}
