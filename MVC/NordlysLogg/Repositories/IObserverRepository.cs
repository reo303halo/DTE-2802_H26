using NordlysLogg.Models;

namespace NordlysLogg.Repositories;

public interface IObserverRepository
{
    Task<IReadOnlyList<Observer>> GetAllAsync();
    Task<int> CountAsync();
}
