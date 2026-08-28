using GameInventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryApi.Data;

// NuGets needed:
// EntityFrameworkCore - EFCore - EF
// EF.Design
// EF.Sqlite
// EF.Tools

// dotnet ef migrations add <name of migration>
// dotnet ef database update

public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().ToTable("Games");
    }
    
    // SEEDING
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSeeding((context, _) =>
        {
            var games = context.Set<Game>();

            if (games.Any()) return;
            games.AddRange(
                new Game
                    { Id = 1, Title = "Elden Ring", Genre = "Action RPG", HoursPlayed = 120, Installed = true },
                new Game
                    { Id = 2, Title = "Stardew Valley", Genre = "Simulation", HoursPlayed = 45, Installed = false },
                new Game
                    { Id = 3, Title = "Cyberpunk 2077", Genre = "RPG", HoursPlayed = 80, Installed = true },
                new Game
                    { Id = 4, Title = "Baldur's Gate 3", Genre = "RPG, Roleplay", HoursPlayed = 230, Installed = true });
            context.SaveChanges();
        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var games = context.Set<Game>();

            if (!await games.AnyAsync(cancellationToken))
            {
                await games.AddRangeAsync(
                    [
                        new Game
                            { Id = 1, Title = "Elden Ring", Genre = "Action RPG", HoursPlayed = 120, Installed = true },
                        new Game
                            { Id = 2, Title = "Stardew Valley", Genre = "Simulation", HoursPlayed = 45, Installed = false },
                        new Game
                            { Id = 3, Title = "Cyberpunk 2077", Genre = "RPG", HoursPlayed = 80, Installed = true },
                        new Game
                            { Id = 4, Title = "Baldur's Gate 3", Genre = "RPG, Roleplay", HoursPlayed = 230, Installed = true }
                    ],
                    cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }
        });
}