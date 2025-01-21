using Microsoft.EntityFrameworkCore;

namespace DbPortfolio.Data
{
    public class DbPortfolioContext : DbContext
    {
        public DbPortfolioContext (DbContextOptions<DbPortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<PortfolioEAI.Models.Projects.Project> Projects { get; set; } = default!;
        public DbSet<PortfolioEAI.Models.Projects.Individual> Individuals { get; set; } = default!;
        public DbSet<PortfolioEAI.Models.Projects.Job> Jobs { get; set; } = default!;
    }
}
