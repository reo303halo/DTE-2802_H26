using PoiMap.Models;

namespace PoiMap.Repositories;

public interface IPoiRepository
{
    Task<IReadOnlyList<PointOfInterest>> GetAllAsync();
    Task<PointOfInterest?> GetByIdAsync(int id);
    Task AddAsync(PointOfInterest poi);
    Task UpdateAsync(PointOfInterest poi);
    Task DeleteAsync(PointOfInterest poi);
}
