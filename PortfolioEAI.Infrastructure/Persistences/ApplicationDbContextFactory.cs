using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PortfolioEAI.Infrastructure.Persistance
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var basePath = ResolveConfigBasePath();
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: false)
                .Build();

            var cs = config.GetConnectionString("MySQLConnection")
                ?? throw new InvalidOperationException("Connection string 'MySQLConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));
            optionsBuilder.UseMySql(cs, serverVersion);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        private static string ResolveConfigBasePath()
        {
            var cwd = Directory.GetCurrentDirectory();

            var candidateFromSolution = Path.Combine(cwd, "PortfolioEAI.Web", "Properties");
            if (Directory.Exists(candidateFromSolution))
                return candidateFromSolution;

            var candidateFromInfrastructure = Path.Combine(cwd, "..", "PortfolioEAI.Web", "Properties");
            if (Directory.Exists(candidateFromInfrastructure))
                return candidateFromInfrastructure;

            throw new DirectoryNotFoundException("Unable to locate PortfolioEAI.Web/Properties for configuration.");
        }
    }
}
