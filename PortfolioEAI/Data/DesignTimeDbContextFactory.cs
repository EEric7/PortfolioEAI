using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PortfolioEAI.Data
{
    public class DesignTimeDbContextFactory
    {
         public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

#if DEBUG
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DevDBContext") ?? throw new InvalidOperationException("Connection string 'DevDBContext' not found."));
#else
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBContext"));
#endif

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}