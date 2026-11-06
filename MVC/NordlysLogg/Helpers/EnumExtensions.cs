using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NordlysLogg.Helpers;

public static class EnumExtensions
{
    /// <summary>Henter [Display(Name = "...")] fra en enum-verdi, f.eks. AuroraColor.Green → "Grønn".</summary>
    public static string GetDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? value.ToString();
    }
}
