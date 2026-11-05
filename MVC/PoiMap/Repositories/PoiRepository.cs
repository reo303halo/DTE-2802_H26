using Microsoft.EntityFrameworkCore;
using PoiMap.Data;
using PoiMap.Models;

namespace PoiMap.Repositories;

public class PoiRepository(AppDbContext context) : IPoiRepository
{
    public async Task<IReadOnlyList<PointOfInterest>> GetAllAsync() =>
        await context.PointsOfInterest
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<PointOfInterest?> GetByIdAsync(int id) =>
        await context.PointsOfInterest.FindAsync(id);

    public async Task AddAsync(PointOfInterest poi)
    {
        context.PointsOfInterest.Add(poi);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PointOfInterest poi)
    {
        context.PointsOfInterest.Update(poi);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(PointOfInterest poi)
    {
        context.PointsOfInterest.Remove(poi);
        await context.SaveChangesAsync();
    }
}
