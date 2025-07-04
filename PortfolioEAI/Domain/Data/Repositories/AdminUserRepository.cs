using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data.Repositories.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;

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
        /// Initializes a new instance of the <see cref="AdminUserRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        /// <param name="logger">The logger to be used for logging operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context or logger is null.</exception>
        public AdminUserRepository(ApplicationDbContext context, ILogger<AdminUserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), Messages.NullError);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.NullError);
        }

        /// <summary>
        /// Adds a new entity.
        /// This method adds the specified AdminUser entity to the database context.
        /// It uses the DbContext to track the new entity and saves the changes to the database.
        /// This method is typically used to create a new admin user in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(AdminUser entity)
        {
            if (entity == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(AdminUser));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddEntityInfo, nameof(AdminUser), entity.Id);
                _context.Set<AdminUser>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddError, nameof(AdminUser), ex.Message);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteNullIdError, nameof(AdminUser));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptEntityInfo, nameof(AdminUser), id);
                var entity = await _context.Set<AdminUser>().FindAsync(id);
                if (entity != null)
                {
                    _logger.LogInformation(Messages.DeleteEntityInfo, nameof(AdminUser), entity.Id);
                    _context.Set<AdminUser>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteError, nameof(AdminUser), ex.Message);
                throw;
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
            try
            {
                _logger.LogInformation(Messages.GetAllEntityInfo, nameof(AdminUser));
                return await _context.Set<AdminUser>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllError, nameof(AdminUser), ex.Message);
                throw;
            }
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
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdError, nameof(AdminUser));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(AdminUser), id);
                return await _context.Set<AdminUser>().FindAsync(id);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.GetError, nameof(AdminUser), id, ex.Message);
                throw;
            }
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
            if (entity == null)
            {
                _logger.LogError(Messages.UpdateNullError, nameof(AdminUser));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }

            if (entity.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(AdminUser));
                throw new ArgumentException(Messages.NullError, nameof(entity.Id));
            }

            try
            {
                _logger.LogInformation(Messages.UpdateEntityInfo, nameof(AdminUser), entity.Id);
                _context.Set<AdminUser>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.UpdateError, nameof(AdminUser), ex.Message);
                throw;
            }
        }
    }
}