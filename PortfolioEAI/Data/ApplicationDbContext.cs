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
            modelBuilder.Entity<Project>()
            .OwnsOne(p => p.Url, url =>
            {
                url.Property(u => u.Value)
                    .HasColumnName("Url");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}