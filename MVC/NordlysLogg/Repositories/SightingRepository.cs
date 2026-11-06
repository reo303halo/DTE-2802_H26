using Microsoft.EntityFrameworkCore;
using NordlysLogg.Data;
using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

public class SightingRepository(NordlysContext context) : ISightingRepository
{
    public async Task<IReadOnlyList<AuroraSighting>> GetAllAsync(int? siteId = null, int? minKp = null, string? sort = null)
    {
        // IQueryable bygges opp steg for steg – SQL sendes først ved ToListAsync()
        IQueryable<AuroraSighting> query = context.Sightings
            .Include(s => s.Site)
            .Include(s => s.Observer)
            .AsNoTracking(); // kun lesing → raskere, ingen endringssporing

        if (siteId is not null)
            query = query.Where(s => s.SiteId == siteId);

        if (minKp is not null)
            query = query.Where(s => s.KpIndex >= minKp);

        query = sort switch
        {
            "oldest" => query.OrderBy(s => s.ObservedAt),
            "kp" => query.OrderByDescending(s => s.KpIndex).ThenByDescending(s => s.ObservedAt),
            _ => query.OrderByDescending(s => s.ObservedAt) // "newest" er standard
        };

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<AuroraSighting>> GetRecentAsync(int count) =>
        await context.Sightings
            .Include(s => s.Site)
            .Include(s => s.Observer)
            .AsNoTracking()
            .OrderByDescending(s => s.ObservedAt)
            .Take(count)
            .ToListAsync();

    public async Task<AuroraSighting?> GetByIdAsync(int id) =>
        await context.Sightings
            .Include(s => s.Site)
            .Include(s => s.Observer)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<AuroraSighting?> GetMostSpectacularAsync()
    {
        // WowScore er [NotMapped] og kan ikke oversettes til SQL.
        // Derfor hentes data først, og sorteringen skjer i minnet (LINQ to Objects).
        // Et godt diskusjonspunkt: hva skjer med ytelsen når tabellen har en million rader?
        var all = await context.Sightings
            .Include(s => s.Site)
            .Include(s => s.Observer)
            .AsNoTracking()
            .ToListAsync();

        return all.MaxBy(s => s.WowScore);
    }

    public Task<int> CountAsync() => context.Sightings.CountAsync();

    public async Task AddAsync(AuroraSighting sighting)
    {
        context.Sightings.Add(sighting);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AuroraSighting sighting)
    {
        // Er entiteten allerede sporet (hentet med GetByIdAsync), finner EF endringene selv.
        // Er den frakoblet (f.eks. bygget fra et skjema), må vi si fra med Update().
        if (context.Entry(sighting).State == EntityState.Detached)
            context.Sightings.Update(sighting);

        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sighting = await context.Sightings.FindAsync(id);
        if (sighting is null) return false;

        context.Sightings.Remove(sighting);
        await context.SaveChangesAsync();
        return true;
    }
}
