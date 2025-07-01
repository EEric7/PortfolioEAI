using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data;
using PortfolioEAI.Data.Repositories;
using PortfolioEAI.Domain.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Entity Framework Core with SQLite based on the environment
if (builder.Environment.IsDevelopment())
{
    // Use a different connection string for development
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DevDBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));
}
else if (builder.Environment.IsStaging())
{
    // Use a different connection string for staging
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StagingDBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));
}
else if (builder.Environment.IsProduction())
{
    // Use the production connection string
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));
}
else
{
    throw new InvalidOperationException("Unknown environment configuration.");
}

// Register repositories and services
builder.Services.AddScoped<IGenericRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IGenericServices<ProjectDto>, ProjectService>();

// Register the main repository interface
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
