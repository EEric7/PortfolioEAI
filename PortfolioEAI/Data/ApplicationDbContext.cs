using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Represents the collection of Project entities in the database.
        /// </summary>
        /// <remarks>
        /// This DbSet allows for querying and saving instances of the Project entity.
        /// </remarks>
         public DbSet<Project> Projects { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// It allows the DbContext to be configured with options such as connection strings, logging,
        /// and other database-related settings.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Configures the model for the ApplicationDbContext.
        /// This method is called by the framework to configure the model that will be used by the
        /// ApplicationDbContext. It allows for customization of the entity mappings, relationships,
        /// and other model-related configurations. 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// Configure the Project entity
            /// </summary>
            modelBuilder.Entity<Project>()
            .OwnsOne(p => p.Url).Property(y => y.Value)
            .HasColumnName("Url"); ;

            /// Configure the Project entity
            base.OnModelCreating(modelBuilder);
        }
    }
}