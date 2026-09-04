using ComputerApi.Data;
using ComputerApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
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

// 1. Identity API Endpoints
// Register UserManager, SignInManager, RoleManager,
// Bearer token auth, and all endpoint services
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ComputerDbContext>();

// 2. Authorization
builder.Services.AddAuthorization();

builder.Services.AddScoped<IComputerService, ComputerService>();

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // /openapi/v1.json
    app.MapScalarApiReference(); // /scalar
}

app.UseHttpsRedirection();

// 4. Map all 10 Identity endpoints: /register, /login, /refresh, etc
app.MapIdentityApi<IdentityUser>();

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