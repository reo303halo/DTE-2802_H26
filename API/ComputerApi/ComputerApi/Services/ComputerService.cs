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

    public async Task<ComputerDto> CreateAsync(CreateComputerDto dto, string ownerId)
    {
        var computer = new Computer
        {
            Model = dto.Model,
            Processor = dto.Processor,
            RamGb = dto.RamGb,
            StorageGb = dto.StorageGb,
            BrandId = dto.BrandId,
            OsId = dto.OsId,
            OwnerId = ownerId
        };

        context.Computers.Add(computer);
        await context.SaveChangesAsync();

        // Reload with navigation properties for the DTO
        await context.Entry(computer).Reference(c => c.Brand).LoadAsync();
        await context.Entry(computer).Reference(c => c.Os).LoadAsync();

        return new ComputerDto
        {
            Id = computer.Id,
            Model = computer.Model,
            Processor = computer.Processor,
            RamGb = computer.RamGb,
            StorageGb = computer.StorageGb,
            Brand = new BrandDto
            {
                Id = computer.Brand.Id,
                Name = computer.Brand.Name,
                Country = computer.Brand.Country
            },
            Os = new OsDto
            {
                Id = computer.Os.Id,
                Name = computer.Os.Name,
                Version = computer.Os.Version
            }
        };
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var computer = await context.Computers.FindAsync(id);

        if (computer == null || computer.OwnerId != userId)
            return false;

        context.Computers.Remove(computer);
        await context.SaveChangesAsync();
        return true;
    }
}