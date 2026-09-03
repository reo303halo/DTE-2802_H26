using ComputerApi.Models;

namespace ComputerApi.Services;

public interface IComputerService
{
    Task<List<ComputerDto>> GetAllAsync();
}