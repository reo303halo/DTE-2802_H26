using CupcakeMVC.Data;
using CupcakeMVC.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICupcakeRepository, CupcakeRepository>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

// DB Services
builder.Services.AddDbContext<CupcakeDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<CupcakeDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Cupcake}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();