using NordlysLogg.Models;

namespace NordlysLogg.ViewModels;

/// <summary>Et sted + statistikk beregnet fra observasjonene. Viewet slipper å regne selv.</summary>
public class SiteDetailsViewModel
{
    public required ObservationSite Site { get; init; }

    public int SightingCount { get; init; }
    public double AverageKp { get; init; }
    public AuroraSighting? BestSighting { get; init; }

    /// <summary>Andel (0–100 %) per farge, til et lite stolpediagram.</summary>
    public IReadOnlyList<ColorShare> ColorDistribution { get; init; } = [];

    public IReadOnlyList<AuroraSighting> Sightings { get; init; } = [];

    public string MapUrl =>
        FormattableString.Invariant(
            $"https://www.openstreetmap.org/?mlat={Site.Latitude}&mlon={Site.Longitude}#map=11/{Site.Latitude}/{Site.Longitude}");

    public static SiteDetailsViewModel Create(ObservationSite site)
    {
        var sightings = site.Sightings.OrderByDescending(s => s.ObservedAt).ToList();

        // Sørg for at hvert kort kan vise stedsnavnet (navigasjonen "tilbake" til stedet)
        foreach (var s in sightings) s.Site ??= site;
        int count = sightings.Count;

        return new SiteDetailsViewModel
        {
            Site = site,
            Sightings = sightings,
            SightingCount = count,
            AverageKp = count == 0 ? 0 : sightings.Average(s => s.KpIndex),
            BestSighting = sightings.MaxBy(s => s.WowScore),
            ColorDistribution = sightings
                .GroupBy(s => s.DominantColor)
                .Select(g => new ColorShare(g.Key, g.Count(), (int)Math.Round(100.0 * g.Count() / count)))
                .OrderByDescending(c => c.Count)
                .ToList()
        };
    }
}

public record ColorShare(AuroraColor Color, int Count, int Percent);
