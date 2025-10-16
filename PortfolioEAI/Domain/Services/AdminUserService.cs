using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Services.Interfaces;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Domain.Services
{
    public class AdminUserService : IAdminUserService
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<AdminUserService> _logger;

        /// <summary>
        /// Service for managing AdminUsers.
        /// </summary>
        private readonly IRepository _repository;

        public AdminUserService(ILogger<AdminUserService> logger, IRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.NullError);
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), Messages.NullError);
        }

        /// <summary>
        /// Retrieves all AdminUsers.
        /// This method fetches all AdminUsers from the repository.
        /// It returns an enumerable collection of <see cref="AdminUser"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdminUser>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(AdminUser));
                return await _repository.AdminUsers.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(AdminUser), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser?> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(AdminUser), id);
                return await _repository.AdminUsers.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(AdminUser), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="AdminUser"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(AdminUser entity)
        {
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(AdminUser), entity.Id);
                await _repository.AdminUsers.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(AdminUser), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="AdminUser"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the entity. It throws an <see cref="ArgumentNullException"/> if the entity is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(AdminUser entity)
        {
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(AdminUser), entity.Id);
                var existing = await _repository.AdminUsers.GetByIdAsync(entity.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(AdminUser), entity.Id);
                    return;
                }

                if (entity.Username != existing.Username)
                    existing.SetUsername(entity.Username);

                if (entity.Password != existing.Password)
                    existing.SetPassword(entity.Password);

                if (entity.Email != existing.Email)
                    existing.Email.SetValue(entity.Email);

                if (entity.Description != existing.Description)
                    existing.SetDescription(entity.Description);

                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(AdminUser), entity.Id);
                await _repository.AdminUsers.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(AdminUser), entity.Id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a project by its identifier.
        /// This method removes a project from the repository using its unique identifier.
        /// It does not return any value.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// It is typically used to remove AdminUsers that are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(AdminUser), id);
                var existing = await _repository.AdminUsers.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(AdminUser), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(AdminUser), id);
                await _repository.AdminUsers.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(AdminUser), ex.Message);
                throw;
            }
        }
    }
}