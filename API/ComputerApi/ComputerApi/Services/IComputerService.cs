using ComputerApi.Models;

namespace ComputerApi.Services;

public interface IComputerService
{
    Task<List<ComputerDto>> GetAllAsync();
    Task<ComputerDto> CreateAsync(CreateComputerDto dto, string ownerId);
    Task<bool> DeleteAsync(int id, string userId);
}