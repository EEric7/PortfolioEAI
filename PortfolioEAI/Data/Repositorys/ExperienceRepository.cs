using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Data.Repositorys
{
    public class ExperienceRepository : IExperienceRepository
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<ExperienceRepository> _logger;

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
        /// <param name="logger">The logger to be used for logging operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context or logger is null.</exception>
        public ExperienceRepository(ApplicationDbContext context, ILogger<ExperienceRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null");
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
             if (entity == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(Experience));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }

            try
            {
                _logger.LogInformation(Messages.AddEntityInfo, nameof(Experience), entity.Id);
                _context.Set<Experience>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddError, nameof(Experience), ex.Message);
                throw;
            }
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
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteNullIdError, nameof(Experience));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }

            try
            {
                _logger.LogInformation(Messages.DeleteAttemptEntityInfo, nameof(Experience), id);
                var entity = await _context.Set<Experience>().FindAsync(id);
                if (entity != null)
                {
                    _logger.LogInformation(Messages.DeleteEntityInfo, nameof(Experience), entity.Id);
                    _context.Set<Experience>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteError, nameof(Experience), ex.Message);
                throw;
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
            try
            {
                _logger.LogInformation(Messages.GetAllEntityInfo, nameof(Experience));
                return await _context.Set<Experience>()
                    .Include(e => e.Projects) // Include related Projects
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllError, nameof(Experience), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// This method retrieves an Experience entity from the database using its unique identifier (GUID).
        /// It uses the DbContext to find the entity and returns it if found, or null if not found.
        /// This method is typically used to retrieve a specific experience by its ID.
        public async Task<Experience?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdError, nameof(Experience));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }

            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(Experience), id);
                return await _context.Set<Experience>()
                    .Include(e => e.Projects) // Include related Projects
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetError, nameof(Experience), id, ex.Message);
                throw;
            }
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
            if (entity == null)
            {
                _logger.LogError(Messages.UpdateNullError, nameof(Experience));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }

            if (entity.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(Experience));
                throw new ArgumentException(Messages.NullError, nameof(entity.Id));
            }

            try
            {
                _logger.LogInformation(Messages.UpdateEntityInfo, nameof(AdminUser), entity.Id);
                _context.Set<Experience>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateError, nameof(AdminUser), ex.Message);
                throw;
            }
        }
    }
}