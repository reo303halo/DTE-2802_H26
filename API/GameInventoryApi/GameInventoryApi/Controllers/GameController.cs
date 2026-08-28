using GameInventoryApi.Models;
using GameInventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameInventoryApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameService gameService) : ControllerBase
{
    // GET: /Game
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetGames()
    {
        var games = await gameService.GetGames();
        
        return Ok(games); // Status Code: 200
    }

    // GET: /Game/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game = await gameService.GetGame(id);

        if (game is null)
            return NotFound();

        return Ok(game);
    }
    
    // POST: /Game
    [HttpPost]
    public async Task<ActionResult<Game>> AddGame(GameDto gameDto)
    {
        var game = await gameService.AddGame(gameDto);
        
        return CreatedAtAction(
            nameof(GetGame),
            new { id = game.Id },
            game
        ); // Status Code: 201
    }
    
    // PUT: /Game/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGame(int id, GameDto gameDto)
    {
        var updated = await gameService.UpdateGame(id, gameDto);

        if (!updated)
            return NotFound($"Game with {id} not found.");

        return Ok("Game successfully updated!");
    }
    
    // DELETE: /Game/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var deleted = await gameService.DeleteGame(id);

        if (!deleted)
            return NotFound($"Game with {id} not found.");
        
        return Ok("Game successfully deleted.");
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