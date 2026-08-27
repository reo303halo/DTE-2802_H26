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
    public ActionResult<Game> AddGame(GameDto gameDto)
    {
        var game = new Game
        {
            Id = Games.Count == 0 ? 1 : Games.Max(g => g.Id) + 1,
            Title = gameDto.Title,
            Genre = gameDto.Genre,
            Installed = gameDto.Installed
        };
        // We assume "AddGame" is when you buy a new game and it to your library.
        // In this case it makes sense that HoursPlayed starts at 0.
        
        Games.Add(game);

        return CreatedAtAction(
            nameof(GetGame),
            new { id = game.Id },
            game
        ); // Status Code: 201
    }
    
    // PUT: /Game/1
    [HttpPut("{id:int}")]
    public IActionResult UpdateGame(int id, GameDto gameDto)
    {
        // if (!ModelState.IsValid) - Now automatic, so this line is not necessary anymore.
        
        var game = Games.FirstOrDefault(g => g.Id == id);
        
        if (game is null)
            return NotFound(); // For a one-liner, this is a cleaner option (compared to GetGame and DeleteGame - {}).

        game.Title = gameDto.Title;
        game.Genre = gameDto.Genre;
        game.HoursPlayed = gameDto.HoursPlayed;
        game.Installed = gameDto.Installed;
        
        return NoContent(); // Ok() if you want to add a message like: Ok("Game was successfully updated")
    }
    
    // In our scenario we might also or separately want a PUT that only increment hours for each hour played.
    
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


// ─────────────────────────────────────────────────────────────────
// IActionResult vs ActionResult<T>
// ─────────────────────────────────────────────────────────────────
// IActionResult
//   - Return type info : opaque — "some HTTP result"
//   - Swagger/OpenAPI   : can't infer response schema automatically
//   - Syntax            : must wrap every return in Ok(x), NotFound(), etc.
//   - Use when          : action has no meaningful success payload
//                          (e.g. DeleteGame -> just returns 200/204/404)
//
// ActionResult<T>
//   - Return type info : typed — "either a T or an error result"
//   - Swagger/OpenAPI   : infers the 200 response schema as T
//   - Syntax            : can `return game;` directly (implicit -> Ok(game))
//                          or still use NotFound(), CreatedAtAction(), etc.
//   - Use when          : action returns a specific data type on success
//                          (e.g. GetGame, GetGames, AddGame -> return Game)
//
// Rule of thumb: returning data?  -> ActionResult<T>
//                no data / status only? -> IActionResult
// ─────────────────────────────────────────────────────────────────