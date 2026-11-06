using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

public interface ISiteRepository
{
    Task<IReadOnlyList<ObservationSite>> GetAllAsync();
    Task<ObservationSite?> GetByIdWithSightingsAsync(int id);
    Task<int> CountAsync();
    Task AddAsync(ObservationSite site);
}
