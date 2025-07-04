using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositories.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Repositories
{
    public class ProjectRepository : IProjectRepositorie
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<AdminUserRepository> _logger;

        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ARepositorie{TEntity}"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context"></param>
        public ProjectRepository(ApplicationDbContext context, ILogger<AdminUserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null");
        }

        /// <summary>
        /// Adds a new entity.
        /// This method adds the specified Project entity to the database context.
        /// It uses the DbContext to track the new entity and saves the changes to the database.
        /// This method is typically used to create a new project in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(Project entity)
        {
            _logger.LogInformation("Ajout d'un nouvel Experience avec Id {Id}", entity.Id);
            _context.Set<Project>().Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// This method retrieves the entity from the database using its identifier and removes it.
        /// If the entity is found, it is removed from the context and changes are saved to the database.
        /// If the entity is not found, no action is taken.
        /// This method is typically used to delete an existing entity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<Project>().FindAsync(id);
            if (entity != null)
            {
                _logger.LogInformation("Adding a new Project with Id {Id}", entity.Id);
                _context.Set<Project>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all entities.
        /// This method retrieves all entities of type Project from the database.
        /// It uses the DbContext to query the database and returns a list of all Project entities.
        /// </summary>
        /// <returns>A list of all Project entities.</returns>
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all Project");
            return await _context.Set<Project>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// This method retrieves a Project entity from the database using its unique identifier (GUID).
        /// It uses the DbContext to find the entity and returns it if found, or null if not found.
        /// This method is typically used to retrieve a specific project by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Project?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving Project with Id {Id}", id);
            return await _context.Set<Project>().FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity.
        /// This method updates the specified Project entity in the database.
        /// It uses the DbContext to track changes to the entity and saves those changes to the database.
        /// This method is typically used to modify an existing project with new data.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Project entity)
        {
            _logger.LogInformation("Updating Project with Id {Id}", entity.Id);
            _context.Set<Project>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}