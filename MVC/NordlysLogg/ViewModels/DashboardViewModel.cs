using NordlysLogg.Models;

namespace NordlysLogg.ViewModels;

/// <summary>Samler data fra flere repositories til forsiden.</summary>
public class DashboardViewModel
{
    public int TotalSightings { get; init; }
    public int SiteCount { get; init; }
    public int ObserverCount { get; init; }
    public AuroraSighting? MostSpectacular { get; init; }
    public IReadOnlyList<AuroraSighting> Recent { get; init; } = [];

    /// <summary>Litt sesongbasert stemning på forsiden.</summary>
    public string SeasonMessage => DateTime.Now.Month switch
    {
        11 or 12 or 1 => "Mørketid – den beste tiden for nordlys. Kle deg godt!",
        9 or 10 or 2 or 3 => "Nordlyssesongen er i gang. Følg med på Kp-varselet.",
        _ => "Midnattssol – nordlyset er der, men du ser det ikke. Planlegg høsten!"
    };
}
