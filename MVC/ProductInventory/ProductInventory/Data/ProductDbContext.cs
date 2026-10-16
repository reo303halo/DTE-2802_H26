using Microsoft.EntityFrameworkCore;
using ProductInventory.Models.Entities;

// dotnet ef Migrations add <name of migration>
// dotnet ef database update

namespace ProductInventory.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>().ToTable("Products");
        
        // SEEDING
        
        // Manufacturers
        modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer { Id = 1, Name = "Wurth", Description = "Wurth Norge er til for kundens verdiskapning", Address = "Gjelleråsen Næringspark, Morteveien 12, 1481 Hagan, NORWAY" });
        modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer { Id = 2, Name = "Chevrolet", Description = "Find New Roads", Address = "Detroit, Michigan, USA" });
        modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer { Id = 3, Name = "Tine", Description = "Meieriprodukter", Address = "Moelv, NORWAY" });
        
        // Category
        modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Tool", Description = "Tools for you" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Vehicles", Description = "All types of Vehicles" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Groceries", Description = "Daily groceries" });
        
        // Products
        modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Hammer", Price = 121.50m, CategoryId = 1, ManufacturerId = 1});
        modelBuilder.Entity<Product>().HasData(new Product { Id = 2, Name = "Angle grinder", Price = 1520.00m, CategoryId = 1, ManufacturerId = 1});
        modelBuilder.Entity<Product>().HasData(new Product { Id = 3, Name = "Suburban", Price = 990000m, CategoryId = 2, ManufacturerId = 2});
        modelBuilder.Entity<Product>().HasData(new Product { Id = 4, Name = "Camaro", Price = 620000m, CategoryId = 2, ManufacturerId = 2});
        modelBuilder.Entity<Product>().HasData(new Product { Id = 5, Name = "Milk", Price = 14.50m, CategoryId = 3, ManufacturerId = 3});
    }

    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}