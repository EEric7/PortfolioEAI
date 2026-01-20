using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Infrastructure.Persistance.Configurations;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        #region DbSets
        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<Experience> Experiences => Set<Experience>();
        #endregion

        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class.
        /// This constructor is used to create an instance of the ApplicationDbContext with the specified options.
        /// The options are typically provided by the dependency injection container and include configurations
        /// such as the database provider, connection string, and other settings.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Configures the model for the ApplicationDbContext.
        /// This method is called by the framework to configure the model that will be used by the
        /// ApplicationDbContext. It applies the entity configurations defined in the separate configuration classes.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the entity mappings and relationships
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ExperienceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            
            // Additional configurations can be added here as needed
            base.OnModelCreating(modelBuilder);
        }
    }
}