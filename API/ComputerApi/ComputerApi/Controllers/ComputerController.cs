using ComputerApi.Models.Entities;
using ComputerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerApi.Controllers;

[ApiController]
[Route("[controller]")] // /Computer
public class ComputerController(IComputerService computerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Computer>>> GetAll()
    {
        var computers = await computerService.GetAllAsync();
        
        return Ok(computers);
    }
}