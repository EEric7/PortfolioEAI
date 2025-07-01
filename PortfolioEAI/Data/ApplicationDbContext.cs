using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data
{
    public class ApplicationDbContext : DbContext
    {
         public DbSet<Project> Projects { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// Configure the Project entity
            /// </summary>
            modelBuilder.Entity<Project>()
            .OwnsOne(p => p.Url).Property(y => y.Value)
            .HasColumnName("Url");;

            base.OnModelCreating(modelBuilder);
        }
    }
}