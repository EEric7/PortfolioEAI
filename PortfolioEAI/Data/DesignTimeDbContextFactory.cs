using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PortfolioEAI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Get the environment variable for ASP.NET Core environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            // Use the appsettings.json file to configure the DbContext
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Properties"))
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            // Create the DbContextOptionsBuilder and configure it to use SQLite using the connection string from the configuration file.
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Use the connection string from the configuration and log SQL commands to the console.
            optionsBuilder
                .UseSqlite(config.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}