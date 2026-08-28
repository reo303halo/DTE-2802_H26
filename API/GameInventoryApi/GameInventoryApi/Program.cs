using GameInventoryApi.Data;
using GameInventoryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddOpenApi();

// 1. Register a CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIDemos", policy =>
    {
        policy.AllowAnyOrigin() // Fine for a demo: In real apps, list specific origins
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlite("Data Source=games.db"));

builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // /openapi/v1.json
}

app.UseHttpsRedirection();

// 2. Use policy (Must be before UseAuthorization() and MapControllers())
app.UseCors("AllowUIDemos");

app.UseAuthorization();
app.MapControllers();
app.Run();

/*
 Can also be run from terminal with:
    dotnet run
or
    dotnet run --launch-profile https
*/