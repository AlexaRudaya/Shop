using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationCore;
using Shop.ApplicationCore.Identity;
using Shop.ApplicationCore.Interfaces;
using Shop.ApplicationCore.Services;
using Shop.Configuration;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Data.Queries;
using Shop.Interfaces;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));

Shop.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

//Configure Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
builder.Services.AddSingleton<IUriComposer>(new UriComposer(builder.Configuration.Get<CatalogSettings>()));
builder.Services.AddScoped<IBasketViewModelService, BasketViewModelService>();
builder.Services.AddScoped<IBasketQueryService, BasketQueryService>();


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

        var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        if (identityContext.Database.IsSqlServer())
        {
            identityContext.Database.Migrate();
        }
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
