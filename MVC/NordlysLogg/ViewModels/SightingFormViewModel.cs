using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using NordlysLogg.Models;

namespace NordlysLogg.ViewModels;

/// <summary>
/// Brukes av både Create og Edit.
/// Hvorfor ikke bruke AuroraSighting direkte i skjemaet?
///  1) Skjemaet trenger nedtrekkslister (Sites/Observers) som ikke hører hjemme i domenemodellen.
///  2) Vi bestemmer selv hvilke felter som kan bindes → beskytter mot "overposting".
///  3) Valideringsregler for skjermbildet (ikke i fremtiden) holdes unna databasemodellen.
/// </summary>
public class SightingFormViewModel : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Når så du nordlyset?")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Tidspunkt")]
    public DateTime ObservedAt { get; set; } = DateTime.Now.Date.AddHours(22);

    [Range(0, 9, ErrorMessage = "Kp-indeksen går fra 0 til 9.")]
    [Display(Name = "Kp-indeks")]
    public int KpIndex { get; set; } = 3;

    [Display(Name = "Intensitet")]
    public AuroraIntensity Intensity { get; set; } = AuroraIntensity.Moderate;

    [Display(Name = "Dominerende farge")]
    public AuroraColor DominantColor { get; set; } = AuroraColor.Green;

    [Range(0, 100, ErrorMessage = "Skydekke oppgis i prosent (0–100).")]
    [Display(Name = "Skydekke (%)")]
    public int CloudCoverPercent { get; set; } = 20;

    [StringLength(1000)]
    [Display(Name = "Notater")]
    public string? Notes { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Velg et sted.")]
    [Display(Name = "Sted")]
    public int SiteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Velg en observatør.")]
    [Display(Name = "Observatør")]
    public int ObserverId { get; set; }

    // Nedtrekkslister – fylles av controlleren, postes ikke tilbake
    [ValidateNever] public IEnumerable<SelectListItem> Sites { get; set; } = [];
    [ValidateNever] public IEnumerable<SelectListItem> Observers { get; set; } = [];

    public bool IsEdit => Id != 0;

    /// <summary>Egendefinert validering på tvers av felter / mot "nå".</summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ObservedAt > DateTime.Now.AddMinutes(5))
            yield return new ValidationResult(
                "Du kan ikke logge nordlys fra fremtiden – selv om varselet ser lovende ut!",
                [nameof(ObservedAt)]);

        if (Intensity == AuroraIntensity.Corona && KpIndex < 3)
            yield return new ValidationResult(
                "Korona ved Kp under 3 er svært uvanlig. Sjekk Kp-verdien.",
                [nameof(KpIndex)]);
    }

    // ---------- Mapping mellom ViewModel og entitet ----------

    public static SightingFormViewModel FromEntity(AuroraSighting s) => new()
    {
        Id = s.Id,
        ObservedAt = s.ObservedAt,
        KpIndex = s.KpIndex,
        Intensity = s.Intensity,
        DominantColor = s.DominantColor,
        CloudCoverPercent = s.CloudCoverPercent,
        Notes = s.Notes,
        SiteId = s.SiteId,
        ObserverId = s.ObserverId
    };

    public AuroraSighting ToEntity() => ApplyTo(new AuroraSighting());

    /// <summary>Kopierer kun de feltene skjemaet har lov til å endre.</summary>
    public AuroraSighting ApplyTo(AuroraSighting s)
    {
        s.ObservedAt = ObservedAt;
        s.KpIndex = KpIndex;
        s.Intensity = Intensity;
        s.DominantColor = DominantColor;
        s.CloudCoverPercent = CloudCoverPercent;
        s.Notes = Notes;
        s.SiteId = SiteId;
        s.ObserverId = ObserverId;
        return s;
    }
}
