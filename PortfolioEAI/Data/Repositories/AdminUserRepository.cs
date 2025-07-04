using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositories.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Data.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
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
        public AdminUserRepository(ApplicationDbContext context, ILogger<AdminUserRepository> logger)
        {
            context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null");
        }

        public async Task AddAsync(AdminUser entity)
        {
            _logger.LogInformation("Ajout d'un nouvel AdminUser avec Id {Id}", entity.Id);
            _context.Set<AdminUser>().Add(entity);
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
            var entity = await _context.Set<AdminUser>().FindAsync(id);
            if (entity != null)
            {
                _logger.LogInformation("Adding a new AdminUser with Id {Id}", entity.Id);
                _context.Set<AdminUser>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all entities.
        /// This method retrieves all entities of type AdminUser from the database.
        /// It uses the DbContext to query the database and returns a list of all AdminUser entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdminUser>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all AdminUsers");
            return await _context.Set<AdminUser>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// This method retrieves an AdminUser entity from the database using its unique identifier (GUID).
        /// It uses the DbContext to find the entity and returns it if found, or null if not found.
        /// This method is typically used to retrieve a specific admin user by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving AdminUser with Id {Id}", id);
            return await _context.Set<AdminUser>().FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity.
        /// This method updates the specified AdminUser entity in the database.
        /// It uses the DbContext to track changes to the entity and saves those changes to the database.
        /// This method is typically used to modify an existing admin user with new data.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(AdminUser entity)
        {
            _logger.LogInformation("Updating AdminUser with Id {Id}", entity.Id);
            _context.Set<AdminUser>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}