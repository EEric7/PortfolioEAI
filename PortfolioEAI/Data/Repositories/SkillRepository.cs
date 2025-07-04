using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositories.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<SkillRepository> _logger;

        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public SkillRepository(ApplicationDbContext context, ILogger<SkillRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null");
        }

        /// <summary>
        /// Adds a new entity.
        /// This method adds the specified Skill entity to the database context.
        /// It uses the DbContext to track the new entity and saves the changes to the database.
        /// This method is typically used to create a new skill in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(Skill entity)
        {
            _logger.LogInformation("Ajout d'un nouvel Skill avec Id {Id}", entity.Id);
            _context.Set<Skill>().Add(entity);
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
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<Skill>().FindAsync(id);
            if (entity != null)
            {
                _logger.LogInformation("Adding a new Skill with Id {Id}", entity.Id);
                _context.Set<Skill>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all entities.
        /// This method retrieves all entities of type Skill from the database.
        /// It uses the DbContext to query the database and returns a list of all Skill entities.
        /// </summary>
        /// <returns>A list of all Skill entities.</returns>    
        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all Skills");
            return await _context.Set<Skill>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// This method retrieves a Skill entity from the database using its unique identifier (GUID).
        /// It uses the DbContext to find the entity and returns it if found, or null if not found.
        /// This method is typically used to retrieve a specific skill by its ID.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The Skill entity if found; otherwise, null.</returns>
        public async Task<Skill?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving Skill with Id {Id}", id);
            return await _context.Set<Skill>().FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity.
        /// This method updates the specified Skill entity in the database.
        /// It uses the DbContext to track changes to the entity and saves those changes to the database.
        /// This method is typically used to modify an existing skill with new data.
        /// </summary>
        /// <param name="entity">The Skill entity to update.</param>
        /// <returns></returns>
        public async Task UpdateAsync(Skill entity)
        {
            _logger.LogInformation("Updating Skill with Id {Id}", entity.Id);
            _context.Set<Skill>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}