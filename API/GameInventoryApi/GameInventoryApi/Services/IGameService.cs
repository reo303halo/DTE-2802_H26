using GameInventoryApi.Models;

namespace GameInventoryApi.Services;

public interface IGameService
{
    Task<IEnumerable<Game>> GetGames();
    Task<Game?> GetGame(int id);
    Task<Game> AddGame(GameDto gameDto);
    Task<bool> UpdateGame(int id, GameDto gameDto);
    Task<bool> DeleteGame(int id);
}