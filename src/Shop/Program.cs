using Microsoft.EntityFrameworkCore;
using Shop.ApplicationCore.Interfaces;
using Shop.Configuration;
using Shop.Infrastructure.Data;
using Shop.Interfaces;
using Shop.Models;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);

Shop.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();

var app = builder.Build();
app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Database migration running...");
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        await CatalogContextSeed.SeedAsync(catalogContext, app.Logger); 
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Database.");
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
