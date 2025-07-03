using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Application.Services;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data;
using PortfolioEAI.Data.Repositories;
using PortfolioEAI.Data.Repositories.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Entity Framework Core with SQLite based on the environment
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Register repositories
builder.Services.AddScoped<ProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IRepository, Repository>();

// Register services
//builder.Services.AddScoped<IGenericServices<ProjectDto>, ProjectService>();

// Register the main repository interface
var app = builder.Build();

// Configure the HTTP request pipeline.
switch (environment) {
    case "Development":
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            DbInitializer.Initialize(context);
        }
        break;
    case "Staging":
        app.UseExceptionHandler("/Error");
        app.UseHsts();
        break;
    case "Production":
        app.UseExceptionHandler("/Error");
        app.UseHsts();
        break;
    default:
        throw new InvalidOperationException($"Unknown environment: {environment}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
