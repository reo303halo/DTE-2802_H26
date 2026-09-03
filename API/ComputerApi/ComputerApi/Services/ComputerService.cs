using ComputerApi.Data;
using ComputerApi.Models;
using ComputerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Services;

public class ComputerService(ComputerDbContext context) : IComputerService
{
    public async Task<List<ComputerDto>> GetAllAsync()
    {
        return await context.Computers
            .Select(c => new ComputerDto
            {
                Id = c.Id,
                Model = c.Model,
                Processor = c.Processor,
                RamGb = c.RamGb,
                StorageGb = c.StorageGb,

                Brand = new BrandDto
                {
                    Id = c.Brand.Id,
                    Name = c.Brand.Name,
                    Country = c.Brand.Country
                },

                Os = new OsDto
                {
                    Id = c.Os.Id,
                    Name = c.Os.Name,
                    Version = c.Os.Version
                }
            })
            .ToListAsync();
    }
}