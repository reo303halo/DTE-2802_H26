namespace PoiMap.ViewModels;

/// <summary>Read-only POI data, used by the map (as JSON), the list and the delete page.</summary>
public class PoiViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
