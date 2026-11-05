using Microsoft.EntityFrameworkCore;
using PoiMap.Models;

namespace PoiMap.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PointOfInterest> PointsOfInterest => Set<PointOfInterest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PointOfInterest>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);

            // Stores "Nature" rather than 0, which makes the table easier to read.
            entity.Property(p => p.Category).HasConversion<string>().HasMaxLength(30);

            // Coordinates are approximate.
            entity.HasData(
                new PointOfInterest { Id = 1, Name = "Nordkapp", Category = PoiCategory.Landmark, Latitude = 71.1693, Longitude = 25.7838, Description = "The North Cape plateau, a 307 metre high cliff facing the Barents Sea." },
                new PointOfInterest { Id = 2, Name = "Arctic Cathedral", Category = PoiCategory.Culture, Latitude = 69.6481, Longitude = 18.9877, Description = "Tromsdalen Church in Tromsø, known for its glass mosaic." },
                new PointOfInterest { Id = 3, Name = "UiT Campus Narvik", Category = PoiCategory.Education, Latitude = 68.4360, Longitude = 17.4340, Description = "UiT The Arctic University of Norway, Campus Narvik." },
                new PointOfInterest { Id = 4, Name = "Nidaros Cathedral", Category = PoiCategory.Culture, Latitude = 63.4269, Longitude = 10.3969, Description = "Norway's national sanctuary in Trondheim." },
                new PointOfInterest { Id = 5, Name = "Geirangerfjord", Category = PoiCategory.Nature, Latitude = 62.1049, Longitude = 7.0941, Description = "UNESCO-listed fjord in Møre og Romsdal." },
                new PointOfInterest { Id = 6, Name = "Bryggen", Category = PoiCategory.Landmark, Latitude = 60.3975, Longitude = 5.3245, Description = "Historic Hanseatic wharf in Bergen." },
                new PointOfInterest { Id = 7, Name = "Oslo Opera House", Category = PoiCategory.Culture, Latitude = 59.9075, Longitude = 10.7531, Description = "Home of the Norwegian National Opera and Ballet." },
                new PointOfInterest { Id = 8, Name = "Preikestolen", Category = PoiCategory.Nature, Latitude = 58.9864, Longitude = 6.1904, Description = "Pulpit Rock, a cliff 604 metres above Lysefjorden." });
        });
    }
}
