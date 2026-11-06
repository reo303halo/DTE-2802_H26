using NordlysLogg.Models;

namespace NordlysLogg.ViewModels;

/// <summary>Flat visning av et sted i oversikten – bare det kortet trenger.</summary>
public record SiteListItemViewModel(
    int Id,
    string Name,
    string Region,
    int DarknessRating,
    int SightingCount,
    DateTime? LastSighting)
{
    public static SiteListItemViewModel FromEntity(ObservationSite s) => new(
        s.Id,
        s.Name,
        s.Region,
        s.DarknessRating,
        s.Sightings.Count,
        s.Sightings.Count == 0 ? null : s.Sightings.Max(x => x.ObservedAt));
}
