using PortfolioEAI.Infrastructure;
using PortfolioEAI.Application;

// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File(Path.Combine(AppContext.BaseDirectory,"Logs", $"Log-{DateTime.Now:ddMMyyyy}.txt"),
//     rollingInterval: RollingInterval.Day)
//     .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Utilise Serilog comme logger principal
//builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Entity Framework Core based on the environment
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new InvalidOperationException($"Unknown environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

// Load configuration files based on the environment
builder.Configuration
    .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Properties"))
    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

// Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

// Application layer
builder.Services.AddApplication();

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

var app = builder.Build();

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
