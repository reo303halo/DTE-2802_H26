using GameInventoryApi.Data;
using GameInventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameInventoryApi.Services;

public class GameService(GameDbContext context) : IGameService
{
    public async Task<IEnumerable<Game>> GetGames()
    {
        return await context.Games
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Game?> GetGame(int id)
    {
        return await context.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(game => game.Id == id);
    }

    public async Task<Game> AddGame(GameDto gameDto)
    {
        var game = new Game
        {
            Title = gameDto.Title,
            Genre = gameDto.Genre,
            Installed = gameDto.Installed
        };

        context.Games.Add(game);

        await context.SaveChangesAsync();

        return game;
    }

    public async Task<bool> UpdateGame(int id, GameDto gameDto)
    {
        var game = await context.Games.FirstOrDefaultAsync(game => game.Id == id);

        if (game is null)
            return false;
        
        game.Title = gameDto.Title;
        game.Genre = gameDto.Genre;
        game.HoursPlayed = gameDto.HoursPlayed;
        game.Installed = gameDto.Installed;

        await context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DeleteGame(int id)
    {
        var game = await context.Games
            .FirstOrDefaultAsync(game => game.Id == id);

        if (game is null)
            return false;

        context.Games.Remove(game);

        await context.SaveChangesAsync();

        return true;
    }
}