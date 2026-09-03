using ComputerApi.Data;
using ComputerApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddOpenApi();

// 1. Register a CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("Demo", policy =>
    {
        policy.AllowAnyOrigin()   // fine for a teaching demo; in real apps, list specific origins
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ComputerDbContext>(options =>
    options.UseSqlite("Data Source=computers.db"));

builder.Services.AddScoped<IComputerService, ComputerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // /openapi/v1.json
}

app.UseHttpsRedirection();

// 2. Use the policy (must be before UseAuthorization / MapControllers)
app.UseCors("Demo");

app.UseAuthorization();
app.MapControllers();
app.Run();

/*
 Can also be run from terminal with:
    dotnet run
or
    dotnet run --launch-profile https
*/