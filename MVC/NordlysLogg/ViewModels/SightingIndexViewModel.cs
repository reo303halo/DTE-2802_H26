using Microsoft.AspNetCore.Mvc.Rendering;
using NordlysLogg.Models;

namespace NordlysLogg.ViewModels;

/// <summary>Listevisning med filtrering og sortering.</summary>
public class SightingIndexViewModel
{
    public IReadOnlyList<AuroraSighting> Sightings { get; set; } = [];

    // Filterverdier (bindes fra query string: ?siteId=1&minKp=4&sort=kp)
    public int? SiteId { get; set; }
    public int? MinKp { get; set; }
    public string Sort { get; set; } = "newest";

    public IEnumerable<SelectListItem> Sites { get; set; } = [];

    public IEnumerable<SelectListItem> SortOptions { get; } =
    [
        new("Nyeste først", "newest"),
        new("Eldste først", "oldest"),
        new("Høyest Kp", "kp")
    ];

    public bool IsFiltered => SiteId is not null || MinKp is not null;
}
