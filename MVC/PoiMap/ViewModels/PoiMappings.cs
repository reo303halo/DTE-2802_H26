using PoiMap.Models;

namespace PoiMap.ViewModels;

public static class PoiMappings
{
    public static PoiViewModel ToViewModel(this PointOfInterest poi) => new()
    {
        Id = poi.Id,
        Name = poi.Name,
        Description = poi.Description,
        Category = poi.Category.ToString(),
        Latitude = poi.Latitude,
        Longitude = poi.Longitude
    };

    public static PoiFormViewModel ToFormViewModel(this PointOfInterest poi) => new()
    {
        Id = poi.Id,
        Name = poi.Name,
        Description = poi.Description,
        Category = poi.Category,
        Latitude = poi.Latitude,
        Longitude = poi.Longitude
    };

    public static void ApplyTo(this PoiFormViewModel model, PointOfInterest poi)
    {
        poi.Name = model.Name;
        poi.Description = model.Description;
        poi.Category = model.Category;
        poi.Latitude = model.Latitude!.Value;
        poi.Longitude = model.Longitude!.Value;
    }
}
