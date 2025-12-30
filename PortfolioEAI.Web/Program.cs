using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositorys;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Data;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Application.Services;
using PortfolioEAI.Domain.Services.Interfaces;
using PortfolioEAI.Domain.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory,"Logs", $"Log-{DateTime.Now:ddMMyyyy}.txt"),
    rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Utilise Serilog comme logger principal
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Entity Framework Core with SQLite based on the environment
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new InvalidOperationException($"Unknown environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

// Load configuration files based on the environment
builder.Configuration
    .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Properties"))
    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IRepository, Repository>();

// Register Application Services
builder.Services.AddScoped<IDashbordService, DashbordService>();
builder.Services.AddScoped<IHomepageService, HomepageService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

// Register Service Domain
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IServices, Services>();

// Configure authentication and authorization
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyCookieAuth";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.LoginPath = "/SignIn_Page";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorization();

// Register the main repository interface
var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

if (environment != "Development")
{
    app.UseHsts();
}

app.UseExceptionHandler("/Errors/Error");
app.UseStatusCodePagesWithReExecute("/Errors/Error404");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
