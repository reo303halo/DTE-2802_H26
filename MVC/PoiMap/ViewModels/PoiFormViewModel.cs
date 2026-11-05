using System.ComponentModel.DataAnnotations;
using PoiMap.Models;

namespace PoiMap.ViewModels;

/// <summary>Input for the create and edit forms, including the validation rules.</summary>
public class PoiFormViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public PoiCategory Category { get; set; }

    // Nullable so that an empty field fails [Required] instead of silently becoming 0.
    [Required]
    [Range(-90.0, 90.0)]
    public double? Latitude { get; set; }

    [Required]
    [Range(-180.0, 180.0)]
    public double? Longitude { get; set; }
}
