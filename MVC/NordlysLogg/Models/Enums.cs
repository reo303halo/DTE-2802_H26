using System.ComponentModel.DataAnnotations;

namespace NordlysLogg.Models;

/// <summary>Hvor kraftig nordlyset var. Lagres som tekst i databasen (se NordlysContext).</summary>
public enum AuroraIntensity
{
    [Display(Name = "Svakt slør")] Faint = 1,
    [Display(Name = "Tydelige buer")] Moderate = 2,
    [Display(Name = "Dansende bånd")] Strong = 3,
    [Display(Name = "Korona over hodet!")] Corona = 4
}

/// <summary>Dominerende farge i nordlyset.</summary>
public enum AuroraColor
{
    [Display(Name = "Grønn")] Green,
    [Display(Name = "Fiolett")] Violet,
    [Display(Name = "Rød")] Red,
    [Display(Name = "Rosa kant")] Pink,
    [Display(Name = "Hvitaktig")] White
}
