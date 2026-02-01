using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Infrastructure.Persistance;
using PortfolioEAI.Infrastructure.Persistance.Repositories;

namespace PortfolioEAI.Infrastructure
{
    static public class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            if (IsDesignTime())
                return services;

            var cs = config.GetConnectionString("MySQLConnection")
            ?? throw new InvalidOperationException("Connection string 'MySQLConnection' not found.");

            // Configure DbContext with MySQL
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseMySql(cs, serverVersion);
                // Additional DbContext configuration can go here
            });

            // Repositories
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();   
            services.AddScoped<IUserRepository, UserRepository>();

            // Initialize the database (skip during EF Core design-time)
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
#if DEBUG                
            DbInitializer.Initialize(dbContext);
#endif

            return services;
        }

        private static bool IsDesignTime()
        {
            return AppContext.GetData("EFCORE_DESIGN_TIME") is bool isDesignTime && isDesignTime;
        }
    }
}