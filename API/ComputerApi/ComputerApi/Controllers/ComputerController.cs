using System.Security.Claims;
using ComputerApi.Models;
using ComputerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ComputerController(IComputerService computerService) : ControllerBase
{
    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ComputerDto>>> GetAll()
    {
        var computers = await computerService.GetAllAsync();
        return Ok(computers);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ComputerDto>> Create(CreateComputerDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var computer = await computerService.CreateAsync(dto, userId);
        return Ok(computer);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var success = await computerService.DeleteAsync(id, userId);

        if (!success)
            return Forbid();

        return NoContent();
    }
}