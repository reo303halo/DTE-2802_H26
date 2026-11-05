namespace PoiMap.Models;

public class PointOfInterest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PoiCategory Category { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
