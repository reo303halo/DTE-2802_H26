using GameInventoryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameInventoryApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private static readonly List<Game> Games =
    [
        new()
        {
            Id = 1,
            Title = "Elden Ring",
            Genre = "Action RPG",
            HoursPlayed = 120,
            Installed = true
        },
        new()
        {
            Id = 2,
            Title = "Stardew Valley",
            Genre = "Simulation",
            HoursPlayed = 45,
            Installed = false
        },
        new()
        {
            Id = 3,
            Title = "Cyberpunk 2077",
            Genre = "RPG",
            HoursPlayed = 80,
            Installed = true
        }
    ];

    // GET: /Game
    [HttpGet]
    public ActionResult<IEnumerable<Game>> GetGames()
    {
        return Ok(Games); // Status Code: 200
    }

    // GET: /Game/1
    [HttpGet("{id:int}")]
    public ActionResult<Game> GetGame(int id)
    {
        var game = Games.FirstOrDefault(g => g.Id == id);
        
        if (game is null)
        {
            return NotFound(); // Status Code: 404
        }
        
        return Ok(game);
    }
    
    // POST: /Game
    [HttpPost]
    public ActionResult<Game> AddGame(Game game)
    {
        game.Id = Games.Count == 0 ? 1 : Games.Max(g => g.Id) + 1;
        
        Games.Add(game);

        return CreatedAtAction(
            nameof(GetGame),
            new { id = game.Id },
            game
        ); // Status Code: 201
    }
    
    // DELETE: /Game/1
    [HttpDelete("{id:int}")]
    public IActionResult DeleteGame(int id)
    {
        var game = Games.FirstOrDefault(g => g.Id == id);
        
        if (game is null)
        {
            return NotFound();
        }

        Games.Remove(game);
        
        return Ok("Game successfully deleted."); // return NoContent();
    }
}

