using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PoiMap.Data;
using PoiMap.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Scoped: one repository (and one DbContext) per HTTP request.
builder.Services.AddScoped<IPoiRepository, PoiRepository>();

var app = builder.Build();

// Creates the database and seed data on first run, so the project starts without extra steps.
// To use migrations instead, see README.md.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Coordinates are entered with a decimal point. On a machine with a Norwegian locale the
// model binder would otherwise expect a comma and reject values such as 68.43.
var culture = new CultureInfo("en-US");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = [culture],
    SupportedUICultures = [culture]
});

app.UseHttpsRedirection();
app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Poi}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
