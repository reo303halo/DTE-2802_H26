using Microsoft.EntityFrameworkCore;
using NordlysLogg.Data;
using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

public class SiteRepository(NordlysContext context) : ISiteRepository
{
    public async Task<IReadOnlyList<ObservationSite>> GetAllAsync() =>
        await context.Sites
            .Include(s => s.Sightings) // trengs for å telle observasjoner i oversikten
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .ToListAsync();

    public async Task<ObservationSite?> GetByIdWithSightingsAsync(int id) =>
        await context.Sites
            .Include(s => s.Sightings)
                .ThenInclude(sighting => sighting.Observer) // Include på to nivåer
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

    public Task<int> CountAsync() => context.Sites.CountAsync();

    public async Task AddAsync(ObservationSite site)
    {
        context.Sites.Add(site);
        await context.SaveChangesAsync();
    }
}
