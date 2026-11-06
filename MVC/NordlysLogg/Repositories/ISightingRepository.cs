using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

/// <summary>
/// Kontrakten for datatilgang til observasjoner.
/// Controllerne kjenner bare dette grensesnittet – ikke EF Core.
/// Det gjør det enkelt å bytte database eller lage en fake-implementasjon i enhetstester.
/// </summary>
public interface ISightingRepository
{
    Task<IReadOnlyList<AuroraSighting>> GetAllAsync(int? siteId = null, int? minKp = null, string? sort = null);
    Task<IReadOnlyList<AuroraSighting>> GetRecentAsync(int count);
    Task<AuroraSighting?> GetByIdAsync(int id);
    Task<AuroraSighting?> GetMostSpectacularAsync();
    Task<int> CountAsync();
    Task AddAsync(AuroraSighting sighting);
    Task UpdateAsync(AuroraSighting sighting);
    Task<bool> DeleteAsync(int id);
}
