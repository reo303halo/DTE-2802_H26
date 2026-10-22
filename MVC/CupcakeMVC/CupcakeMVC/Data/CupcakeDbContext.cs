using CupcakeMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CupcakeMVC.Data;

public class CupcakeDbContext(DbContextOptions<CupcakeDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    // Users are already defined by IdentityDbContext
    public DbSet<Size> Sizes => Set<Size>();
    public DbSet<Cupcake> Cupcakes => Set<Cupcake>();
    public DbSet<Category> Categories => Set<Category>();

    private const string DefaultUserId = "default-id";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Cupcake>().ToTable("Cupcakes");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSeeding((context, _) =>
            {
                var users = context.Set<IdentityUser>();
                var sizes = context.Set<Size>();
                var categories = context.Set<Category>();
                var cupcakes = context.Set<Cupcake>();

                // USER
                if (!users.Any(u => u.Id == DefaultUserId))
                    users.Add(CreateDefaultUser());

                // SIZES
                if (!sizes.Any())
                    sizes.AddRange(GetSizes());

                // CATEGORIES
                if (!categories.Any())
                    categories.AddRange(GetCategories());

                // CUPCAKES
                if (!cupcakes.Any())
                    cupcakes.AddRange(GetCupcakes());

                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var users = context.Set<IdentityUser>();
                var sizes = context.Set<Size>();
                var categories = context.Set<Category>();
                var cupcakes = context.Set<Cupcake>();

                // USER
                if (!await users.AnyAsync(u => u.Id == DefaultUserId, cancellationToken))
                    await users.AddAsync(CreateDefaultUser(), cancellationToken);

                // SIZES
                if (!await sizes.AnyAsync(cancellationToken))
                    await sizes.AddRangeAsync(GetSizes(), cancellationToken);

                // CATEGORIES
                if (!await categories.AnyAsync(cancellationToken))
                    await categories.AddRangeAsync(GetCategories(), cancellationToken);

                // CUPCAKES
                if (!await cupcakes.AnyAsync(cancellationToken))
                    await cupcakes.AddRangeAsync(GetCupcakes(), cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            });

    //
    // SEED DATA (shared by sync and async seeding)
    //

    private static IdentityUser CreateDefaultUser()
    {
        var user = new IdentityUser
        {
            Id = DefaultUserId,
            UserName = "default@example.com",
            NormalizedUserName = "DEFAULT@EXAMPLE.COM",
            Email = "default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString() // Fine here: runtime seeding, not part of the model
        };

        user.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(user, "DefaultPassword123!"); // Do not hardcode - use env or similar

        return user;
    }

    private static Size[] GetSizes() =>
    [
        new Size { Id = 1, Name = "Bite Size" },
        new Size { Id = 2, Name = "Small" },
        new Size { Id = 3, Name = "Medium" },
        new Size { Id = 4, Name = "Large" },
        new Size { Id = 5, Name = "Almost Cake Size" }
    ];

    private static Category[] GetCategories() =>
    [
        new Category { Id = 1, Name = "Regular" },
        new Category { Id = 2, Name = "Vegan" },
        new Category { Id = 3, Name = "Gluten Free" }
    ];

    // Cupcakes: cupcakes by https://www.ediblearrangements.com/blog/types-of-cupcakes/
    private static Cupcake[] GetCupcakes() =>
    [
        new Cupcake
        {
            Id = 1,
            Name = "Strawberry Shortcake",
            Description = "Stuffed with fresh strawberries and topped with fluffy whipped cream, these strawberry shortcake cupcakes are simply divine. Plus, the vanilla cupcake batter couldn’t be easier to whip up.",
            SizeId = 3,
            CategoryId = 1,
            OwnerId = DefaultUserId
        },
        new Cupcake
        {
            Id = 2,
            Name = "Lemon Cupcakes",
            Description = "Filled with lemon curd and topped with lemon buttercream frosting, these cupcakes are sweet, tangy, and jam-packed with flavor. They taste like lemon drop candies in cupcake form.",
            SizeId = 3,
            CategoryId = 1,
            OwnerId = DefaultUserId
        },
        new Cupcake
        {
            Id = 3,
            Name = "Chocolate Cupcakes with Peanut Butter Frosting",
            Description = "These cupcakes are the ultimate chocolate and peanut butter dessert. A dollop of peanut butter frosting tops moist chocolate cupcakes with chocolate drizzle and mini peanut butter cups.",
            SizeId = 2,
            CategoryId = 1,
            OwnerId = DefaultUserId
        },
        new Cupcake
        {
            Id = 4,
            Name = "Coconut Cupcakes",
            Description = "Have a penchant for coconut? These cupcakes are made with a soft, fluffy vanilla cake topped with a coconut cream cheese buttercream frosting. Toasted coconut sprinkled on top makes the entire cupcake tastier.",
            SizeId = 2,
            CategoryId = 2,
            OwnerId = DefaultUserId
        }
    ];
}