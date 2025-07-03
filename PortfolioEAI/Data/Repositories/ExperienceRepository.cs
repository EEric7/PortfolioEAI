using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositories.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.  
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExperienceRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        /// <returns></returns>
        public ExperienceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new entity.
        /// This method adds the specified Experience entity to the database context.
        /// It uses the DbContext to track the new entity and saves the changes to the database.
        /// This method is typically used to create a new experience in the database.
        /// </summary>
        /// <param name="entity">The Experience entity to add.</param>
        /// <returns></returns>
        public async Task AddAsync(Experience entity)
        {
            _context.Set<Experience>().Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// This method retrieves the entity from the database using its identifier and removes it.
        /// If the entity is found, it is removed from the context and changes are saved to the database.
        /// If the entity is not found, no action is taken.
        /// This method is typically used to delete an existing entity from the database.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var obj = await _context.Set<Experience>().FindAsync(id);
            if (obj != null)
            {
                _context.Set<Experience>().Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// Gets all entities.
        /// This method retrieves all entities of type Experience from the database.
        /// It uses the DbContext to query the database and returns a list of all Experience entities.
        /// </summary>
        /// <returns>A list of all Experience entities.</returns>
        public async Task<IEnumerable<Experience>> GetAllAsync()
        {
            return await _context.Set<Experience>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// This method retrieves an Experience entity from the database using its unique identifier (GUID).
        /// It uses the DbContext to find the entity and returns it if found, or null if not found.
        /// This method is typically used to retrieve a specific experience by its ID.
        public async Task<Experience?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Experience>().FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity.
        /// This method updates the specified Experience entity in the database.
        /// It uses the DbContext to track changes to the entity and saves those changes to the database.
        /// This method is typically used to modify an existing experience with new data.
        /// </summary>
        /// <param name="entity">The Experience entity to update.</param>  
        /// /// <returns></returns>
        public async Task UpdateAsync(Experience entity)
        {
            _context.Set<Experience>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}