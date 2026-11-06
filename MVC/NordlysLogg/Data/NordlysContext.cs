using Microsoft.EntityFrameworkCore;
using NordlysLogg.Models;

namespace NordlysLogg.Data;

public class NordlysContext(DbContextOptions<NordlysContext> options) : DbContext(options)
{
    public DbSet<AuroraSighting> Sightings => Set<AuroraSighting>();
    public DbSet<ObservationSite> Sites => Set<ObservationSite>();
    public DbSet<Observer> Observers => Set<Observer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API: konfigurasjon som ikke passer som attributter på modellen
        modelBuilder.Entity<AuroraSighting>(e =>
        {
            // Lagre enum som lesbar tekst i stedet for tall
            e.Property(s => s.Intensity).HasConversion<string>().HasMaxLength(20);
            e.Property(s => s.DominantColor).HasConversion<string>().HasMaxLength(20);

            e.HasOne(s => s.Site)
             .WithMany(site => site.Sightings)
             .HasForeignKey(s => s.SiteId)
             .OnDelete(DeleteBehavior.Restrict); // et sted med observasjoner kan ikke slettes

            e.HasOne(s => s.Observer)
             .WithMany(o => o.Sightings)
             .HasForeignKey(s => s.ObserverId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(s => s.ObservedAt);
        });

        SeedData.Apply(modelBuilder);
    }
}
