using ComputerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Data;



public class ComputerDbContext(DbContextOptions<ComputerDbContext> options) : DbContext(options)
{
    public DbSet<Computer> Computers => Set<Computer>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Os> OperatingSystems => Set<Os>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Computer>()
            .ToTable("Computers");
        
        /*
         * modelBuilder.Entity<Computer>()
         *  .HasOne(c => c.Brand)
         *  .WithMany(b => b.Computers)
         *  .HasForeignKey(c => c.BrandId);
         */
        
        modelBuilder.Entity<Brand>()
            .ToTable("Brands");

        modelBuilder.Entity<Os>()
            .ToTable("OperatingSystems");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSeeding((context, _) =>
            {
                var brands = context.Set<Brand>();
                var operatingSystems = context.Set<Os>();
                var computers = context.Set<Computer>();

                if (brands.Any())
                    return;

                brands.AddRange(
                    new Brand { Id = 1, Name = "Apple", Country = "United States" },
                    new Brand { Id = 2, Name = "Lenovo", Country = "China" },
                    new Brand { Id = 3, Name = "Dell", Country = "United States" },
                    new Brand { Id = 4, Name = "ASUS", Country = "Taiwan" }
                );

                operatingSystems.AddRange(
                    new Os { Id = 1, Name = "macOS", Version = "Tahoe" },
                    new Os { Id = 2, Name = "Windows", Version = "11" },
                    new Os { Id = 3, Name = "Ubuntu", Version = "24.04" }
                );

                computers.AddRange(
                    new Computer
                    {
                        Id = 1,
                        Model = "MacBook Pro 14",
                        Processor = "Apple M4 Pro",
                        RamGb = 24,
                        StorageGb = 512,
                        BrandId = 1,
                        OsId = 1
                    },
                    new Computer
                    {
                        Id = 2,
                        Model = "ThinkPad X1 Carbon",
                        Processor = "Intel Core Ultra 7",
                        RamGb = 32,
                        StorageGb = 1000,
                        BrandId = 2,
                        OsId = 2
                    },
                    new Computer
                    {
                        Id = 3,
                        Model = "XPS 15",
                        Processor = "Intel Core i7",
                        RamGb = 32,
                        StorageGb = 1000,
                        BrandId = 3,
                        OsId = 2
                    },
                    new Computer
                    {
                        Id = 4,
                        Model = "ROG Zephyrus G14",
                        Processor = "AMD Ryzen 9",
                        RamGb = 32,
                        StorageGb = 1000,
                        BrandId = 4,
                        OsId = 2
                    },
                    new Computer
                    {
                        Id = 5,
                        Model = "ThinkPad T14",
                        Processor = "AMD Ryzen 7",
                        RamGb = 16,
                        StorageGb = 512,
                        BrandId = 2,
                        OsId = 3
                    }
                );

                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var brands = context.Set<Brand>();
                var operatingSystems = context.Set<Os>();
                var computers = context.Set<Computer>();

                if (await brands.AnyAsync(cancellationToken))
                    return;

                await brands.AddRangeAsync(
                    [
                        new Brand { Id = 1, Name = "Apple", Country = "United States" },
                        new Brand { Id = 2, Name = "Lenovo", Country = "China" },
                        new Brand { Id = 3, Name = "Dell", Country = "United States" },
                        new Brand { Id = 4, Name = "ASUS", Country = "Taiwan" }
                    ],
                    cancellationToken);

                await operatingSystems.AddRangeAsync(
                    [
                        new Os { Id = 1, Name = "macOS", Version = "Tahoe" },
                        new Os { Id = 2, Name = "Windows", Version = "11" },
                        new Os { Id = 3, Name = "Ubuntu", Version = "24.04" }
                    ],
                    cancellationToken);

                await computers.AddRangeAsync(
                    [
                        new Computer
                        {
                            Id = 1,
                            Model = "MacBook Pro 14",
                            Processor = "Apple M4 Pro",
                            RamGb = 24,
                            StorageGb = 512,
                            BrandId = 1,
                            OsId = 1
                        },
                        new Computer
                        {
                            Id = 2,
                            Model = "ThinkPad X1 Carbon",
                            Processor = "Intel Core Ultra 7",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 2,
                            OsId = 2
                        },
                        new Computer
                        {
                            Id = 3,
                            Model = "XPS 15",
                            Processor = "Intel Core i7",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 3,
                            OsId = 2
                        },
                        new Computer
                        {
                            Id = 4,
                            Model = "ROG Zephyrus G14",
                            Processor = "AMD Ryzen 9",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 4,
                            OsId = 2
                        },
                        new Computer
                        {
                            Id = 5,
                            Model = "ThinkPad T14",
                            Processor = "AMD Ryzen 7",
                            RamGb = 16,
                            StorageGb = 512,
                            BrandId = 2,
                            OsId = 3
                        }
                    ],
                    cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            });
}
