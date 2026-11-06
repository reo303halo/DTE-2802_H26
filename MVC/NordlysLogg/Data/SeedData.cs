using Microsoft.EntityFrameworkCore;
using NordlysLogg.Models;

namespace NordlysLogg.Data;

/// <summary>Startdata via HasData. Verdiene må være faste (ingen DateTime.Now) og ha eksplisitte Id-er.</summary>
public static class SeedData
{
    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Observer>().HasData(
            new Observer { Id = 1, Name = "Ingrid Solbakken", Nickname = "Kp-jegeren" },
            new Observer { Id = 2, Name = "Mathias Nilsen", Nickname = "Stativ-Mathias" },
            new Observer { Id = 3, Name = "Aiko Tanaka", Nickname = "Første vinter i nord" });

        modelBuilder.Entity<ObservationSite>().HasData(
            new ObservationSite
            {
                Id = 1, Name = "Fjellheisen, Narvik", Region = "Nordland",
                Latitude = 68.4300, Longitude = 17.4520, DarknessRating = 3,
                Description = "Utsikt over Ofotfjorden og byen. Litt lysforurensning, men et fantastisk panorama."
            },
            new ObservationSite
            {
                Id = 2, Name = "Prestvannet, Tromsø", Region = "Troms",
                Latitude = 69.6620, Longitude = 18.9330, DarknessRating = 2,
                Description = "Tilgjengelig midt på Tromsøya. Speilinger i vannet når det er vindstille."
            },
            new ObservationSite
            {
                Id = 3, Name = "Uttakleiv, Lofoten", Region = "Nordland",
                Latitude = 68.2140, Longitude = 13.5320, DarknessRating = 5,
                Description = "Kjent strand med runde steiner. Nesten helt mørkt mot nord."
            },
            new ObservationSite
            {
                Id = 4, Name = "Abisko", Region = "Norrbotten (SE)",
                Latitude = 68.3500, Longitude = 18.8300, DarknessRating = 5,
                Description = "Ligger i regnskyggen bak fjellene – ofte klar himmel når kysten har skyer."
            });

        modelBuilder.Entity<AuroraSighting>().HasData(
            new AuroraSighting { Id = 1, ObservedAt = new DateTime(2026, 9, 28, 22, 15, 0), KpIndex = 4, Intensity = AuroraIntensity.Moderate, DominantColor = AuroraColor.Green, CloudCoverPercent = 20, SiteId = 1, ObserverId = 1, Notes = "Sesongens første ordentlige bue over fjorden." },
            new AuroraSighting { Id = 2, ObservedAt = new DateTime(2026, 10, 3, 23, 40, 0), KpIndex = 7, Intensity = AuroraIntensity.Corona, DominantColor = AuroraColor.Violet, CloudCoverPercent = 5, SiteId = 3, ObserverId = 2, Notes = "Korona rett over hodet. Glemte å puste." },
            new AuroraSighting { Id = 3, ObservedAt = new DateTime(2026, 10, 3, 23, 55, 0), KpIndex = 7, Intensity = AuroraIntensity.Strong, DominantColor = AuroraColor.Red, CloudCoverPercent = 10, SiteId = 4, ObserverId = 3, Notes = "Røde topper på båndene – sjeldent!" },
            new AuroraSighting { Id = 4, ObservedAt = new DateTime(2026, 10, 12, 21, 5, 0), KpIndex = 2, Intensity = AuroraIntensity.Faint, DominantColor = AuroraColor.White, CloudCoverPercent = 60, SiteId = 2, ObserverId = 3, Notes = "Usikker på om det var nordlys eller skyer. Kameraet sa grønt." },
            new AuroraSighting { Id = 5, ObservedAt = new DateTime(2026, 10, 19, 1, 30, 0), KpIndex = 5, Intensity = AuroraIntensity.Strong, DominantColor = AuroraColor.Green, CloudCoverPercent = 15, SiteId = 1, ObserverId = 1 },
            new AuroraSighting { Id = 6, ObservedAt = new DateTime(2026, 10, 26, 20, 45, 0), KpIndex = 3, Intensity = AuroraIntensity.Moderate, DominantColor = AuroraColor.Pink, CloudCoverPercent = 35, SiteId = 2, ObserverId = 2, Notes = "Rosa underkant mens båndet beveget seg raskt." },
            new AuroraSighting { Id = 7, ObservedAt = new DateTime(2026, 11, 2, 22, 0, 0), KpIndex = 6, Intensity = AuroraIntensity.Strong, DominantColor = AuroraColor.Green, CloudCoverPercent = 0, SiteId = 3, ObserverId = 1, Notes = "Speilbilde i bølgene. Beste natta i år?" },
            new AuroraSighting { Id = 8, ObservedAt = new DateTime(2026, 11, 4, 19, 20, 0), KpIndex = 1, Intensity = AuroraIntensity.Faint, DominantColor = AuroraColor.Green, CloudCoverPercent = 80, SiteId = 1, ObserverId = 3 });
    }
}
