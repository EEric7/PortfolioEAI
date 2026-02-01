using PortfolioEAI.Infrastructure;
using PortfolioEAI.Application;

var builder = WebApplication.CreateBuilder(args);

var isDesignTime = AppContext.GetData("EFCORE_DESIGN_TIME") is bool isDesignTimeFlag && isDesignTimeFlag;

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Entity Framework Core based on the environment
var environment = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(Path.Combine(builder.Environment.ContentRootPath, "Properties"))
    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

// Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

// Application layer
builder.Services.AddApplication();

// Configure authentication and authorization
builder.Services.AddAuthentication("PEAICookieAuth")
    .AddCookie("PEAICookieAuth", options =>
    {
        options.Cookie.Name = "PEAICookieAuth";
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
