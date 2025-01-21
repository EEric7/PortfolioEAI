using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DbPortfolio.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DbPortfolioContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DbPortfolioContext")));
}
else
{
    builder.Services.AddDbContext<DbPortfolioContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbPortfolioContext")));
}

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
