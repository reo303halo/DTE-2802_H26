using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NordlysLogg.Data;
using NordlysLogg.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NordlysContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("NordlysDb")));

builder.Services.AddScoped<ISightingRepository, SightingRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IObserverRepository, ObserverRepository>();

var app = builder.Build();

// Opprett databasen ved oppstart.
// Finnes det migrasjoner i prosjektet brukes de; ellers lages skjemaet direkte fra modellen.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NordlysContext>();
    if (db.Database.GetMigrations().Any())
        db.Database.Migrate();
    else
        db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Invariant kultur gjør at tall og datoer fra skjema (f.eks. 68.44) bindes likt
// uansett om maskinen kjører norsk eller engelsk Windows/macOS.
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture),
    SupportedCultures = [CultureInfo.InvariantCulture],
    SupportedUICultures = [CultureInfo.InvariantCulture]
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
