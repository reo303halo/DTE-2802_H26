using Microsoft.EntityFrameworkCore;
using NordlysLogg.Data;
using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

public class ObserverRepository(NordlysContext context) : IObserverRepository
{
    public async Task<IReadOnlyList<Observer>> GetAllAsync() =>
        await context.Observers.AsNoTracking().OrderBy(o => o.Name).ToListAsync();

    public Task<int> CountAsync() => context.Observers.CountAsync();
}
