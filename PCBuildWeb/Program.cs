using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.ViewModels;
using PCBuildWeb.Services.Building;
using PCBuildWeb.Services.Entities.Parts;
using PCBuildWeb.Services.Seeding;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = builder.Configuration["AzureKeyVault:Endpoint"];
var keyVaultTenantId = builder.Configuration["AzureKeyVault:TenantId"];
var keyVaultClientId = builder.Configuration["AzureKeyVault:ClientId"];
var keyVaultClientSecret = builder.Configuration["AzureKeyVault:ClientSecret"];

var credential = new ClientSecretCredential(keyVaultTenantId, keyVaultClientId, keyVaultClientSecret);

var client = new SecretClient(new Uri(keyVaultEndpoint), credential);

builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PCBuildWebContext");

builder.Services.AddTransient<SeedingService>();

builder.Services.AddDbContext<PCBuildWebContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        builder => builder.MigrationsAssembly("PCBuildWeb")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<BuildService>();
builder.Services.AddScoped<CaseFanService>();
builder.Services.AddScoped<CaseService>();
builder.Services.AddScoped<CPUCoolerService>();
builder.Services.AddScoped<CPUService>();
builder.Services.AddScoped<GPUService>();
builder.Services.AddScoped<MemoryService>();
builder.Services.AddScoped<MotherboardService>();
builder.Services.AddScoped<PSUService>();
builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<WC_CPU_BlockService>();
builder.Services.AddScoped<WC_RadiatorService>();
builder.Services.AddScoped<WC_ReservoirService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new ViewLocationExtension());
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
static void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedingService>();
        service.Seed();
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
