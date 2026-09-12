using ComputerApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add <name of migration>
// dotnet ef database update

namespace ComputerApi.Data;

public class ComputerDbContext(DbContextOptions<ComputerDbContext> options)
    : IdentityDbContext<CustomUser>(options)
{
    public DbSet<Computer> Computers => Set<Computer>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Os> OperatingSystems => Set<Os>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Computer>()
            .ToTable("Computers");

        modelBuilder.Entity<Brand>()
            .ToTable("Brands");

        modelBuilder.Entity<Os>()
            .ToTable("OperatingSystems");

        // A Computer has one Owner.
        // A ComputerUser can own many Computers.
        modelBuilder.Entity<Computer>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Computers)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSeeding((context, _) =>
            {
                var users = context.Set<CustomUser>();
                var brands = context.Set<Brand>();
                var operatingSystems = context.Set<Os>();
                var computers = context.Set<Computer>();

                //
                // USER
                //
                var defaultUser = users.FirstOrDefault(
                    u => u.Id == "default-id");

                if (defaultUser == null)
                {
                    defaultUser = CreateDefaultUser();
                    users.Add(defaultUser);
                }

                //
                // BRANDS
                //
                if (!brands.Any())
                {
                    brands.AddRange(
                        new Brand
                        {
                            Id = 1,
                            Name = "Apple",
                            Country = "United States"
                        },
                        new Brand
                        {
                            Id = 2,
                            Name = "Lenovo",
                            Country = "China"
                        },
                        new Brand
                        {
                            Id = 3,
                            Name = "Dell",
                            Country = "United States"
                        },
                        new Brand
                        {
                            Id = 4,
                            Name = "ASUS",
                            Country = "Taiwan"
                        }
                    );
                }

                //
                // OPERATING SYSTEMS
                //
                if (!operatingSystems.Any())
                {
                    operatingSystems.AddRange(
                        new Os
                        {
                            Id = 1,
                            Name = "macOS",
                            Version = "Tahoe"
                        },
                        new Os
                        {
                            Id = 2,
                            Name = "Windows",
                            Version = "11"
                        },
                        new Os
                        {
                            Id = 3,
                            Name = "Ubuntu",
                            Version = "24.04"
                        }
                    );
                }

                //
                // COMPUTERS
                //
                if (!computers.Any())
                {
                    computers.AddRange(
                        new Computer
                        {
                            Id = 1,
                            Model = "MacBook Pro 14",
                            Processor = "Apple M4 Pro",
                            RamGb = 24,
                            StorageGb = 512,
                            BrandId = 1,
                            OsId = 1,
                            OwnerId = defaultUser.Id
                        },
                        new Computer
                        {
                            Id = 2,
                            Model = "ThinkPad X1 Carbon",
                            Processor = "Intel Core Ultra 7",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 2,
                            OsId = 2,
                            OwnerId = defaultUser.Id
                        },
                        new Computer
                        {
                            Id = 3,
                            Model = "XPS 15",
                            Processor = "Intel Core i7",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 3,
                            OsId = 2,
                            OwnerId = defaultUser.Id
                        },
                        new Computer
                        {
                            Id = 4,
                            Model = "ROG Zephyrus G14",
                            Processor = "AMD Ryzen 9",
                            RamGb = 32,
                            StorageGb = 1000,
                            BrandId = 4,
                            OsId = 2,
                            OwnerId = defaultUser.Id
                        },
                        new Computer
                        {
                            Id = 5,
                            Model = "ThinkPad T14",
                            Processor = "AMD Ryzen 7",
                            RamGb = 16,
                            StorageGb = 512,
                            BrandId = 2,
                            OsId = 3,
                            OwnerId = defaultUser.Id
                        }
                    );
                }

                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var users = context.Set<CustomUser>();
                var brands = context.Set<Brand>();
                var operatingSystems = context.Set<Os>();
                var computers = context.Set<Computer>();

                //
                // USER
                //
                var defaultUser = await users.FirstOrDefaultAsync(
                    u => u.Id == "default-id",
                    cancellationToken);

                if (defaultUser == null)
                {
                    defaultUser = CreateDefaultUser();
                    await users.AddAsync(defaultUser, cancellationToken);
                }

                //
                // BRANDS
                //
                if (!await brands.AnyAsync(cancellationToken))
                {
                    await brands.AddRangeAsync(
                        [
                            new Brand
                            {
                                Id = 1,
                                Name = "Apple",
                                Country = "United States"
                            },
                            new Brand
                            {
                                Id = 2,
                                Name = "Lenovo",
                                Country = "China"
                            },
                            new Brand
                            {
                                Id = 3,
                                Name = "Dell",
                                Country = "United States"
                            },
                            new Brand
                            {
                                Id = 4,
                                Name = "ASUS",
                                Country = "Taiwan"
                            }
                        ],
                        cancellationToken);
                }

                //
                // OPERATING SYSTEMS
                //
                if (!await operatingSystems.AnyAsync(cancellationToken))
                {
                    await operatingSystems.AddRangeAsync(
                        [
                            new Os
                            {
                                Id = 1,
                                Name = "macOS",
                                Version = "Tahoe"
                            },
                            new Os
                            {
                                Id = 2,
                                Name = "Windows",
                                Version = "11"
                            },
                            new Os
                            {
                                Id = 3,
                                Name = "Ubuntu",
                                Version = "24.04"
                            }
                        ],
                        cancellationToken);
                }

                //
                // COMPUTERS
                //
                if (!await computers.AnyAsync(cancellationToken))
                {
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
                                OsId = 1,
                                OwnerId = defaultUser.Id
                            },
                            new Computer
                            {
                                Id = 2,
                                Model = "ThinkPad X1 Carbon",
                                Processor = "Intel Core Ultra 7",
                                RamGb = 32,
                                StorageGb = 1000,
                                BrandId = 2,
                                OsId = 2,
                                OwnerId = defaultUser.Id
                            },
                            new Computer
                            {
                                Id = 3,
                                Model = "XPS 15",
                                Processor = "Intel Core i7",
                                RamGb = 32,
                                StorageGb = 1000,
                                BrandId = 3,
                                OsId = 2,
                                OwnerId = defaultUser.Id
                            },
                            new Computer
                            {
                                Id = 4,
                                Model = "ROG Zephyrus G14",
                                Processor = "AMD Ryzen 9",
                                RamGb = 32,
                                StorageGb = 1000,
                                BrandId = 4,
                                OsId = 2,
                                OwnerId = defaultUser.Id
                            },
                            new Computer
                            {
                                Id = 5,
                                Model = "ThinkPad T14",
                                Processor = "AMD Ryzen 7",
                                RamGb = 16,
                                StorageGb = 512,
                                BrandId = 2,
                                OsId = 3,
                                OwnerId = defaultUser.Id
                            }
                        ],
                        cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);
            });

    private static CustomUser CreateDefaultUser()
    {
        var user = new CustomUser
        {
            Id = "default-id",
            UserName = "default@example.com",
            NormalizedUserName = "DEFAULT@EXAMPLE.COM",
            Email = "default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "default-security-stamp"
        };

        var hasher = new PasswordHasher<CustomUser>();

        user.PasswordHash = hasher.HashPassword(
            user,
            "DefaultPassword123!"); // Do not hardcode - Use env or similar

        return user;
    }
}