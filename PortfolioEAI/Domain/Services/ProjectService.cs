using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Services.Interfaces;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Domain.Services
{
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<ProjectService> _logger;
        
        /// <summary>
        /// Service for managing projects.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class with the specified logger and repository.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// It ensures that the logger and repository are not null, throwing an <see cref="ArgumentNullException"/> if they are.
        /// The logger is used for logging operations, while the repository is used for data access operations related to projects.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectService(ILogger<ProjectService> logger, IRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.NullError);
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), Messages.NullError);
        }

        /// <summary>
        /// Retrieves all projects.
        /// This method fetches all projects from the repository and maps them to DTOs.
        /// It returns an enumerable collection of <see cref="Project"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(Project));
                return await _repository.Projects.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(Project), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="Project"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Project?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(AdminUser));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(Project), id);
                return await _repository.Projects.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(Project), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="Project"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the entity is null.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(Project entity)
        {
            if (entity == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(Project));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(Project), entity.Title);
                await _repository.Projects.AddAsync(entity);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(Project), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="Project"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the entity. It throws an <see cref="ArgumentNullException"/> if the entity is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(Project entity)
        {
            if (entity == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(Project));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }
            if (entity.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(Project));
                throw new ArgumentException(Messages.NullError, nameof(entity.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Project), entity.Id);
                var existing = await _repository.Projects.GetByIdAsync(entity.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(Project), entity.Id);
                    return;
                }
                if (entity.Title != null)
                    existing.SetTitle(entity.Title);

                if (entity.ImageUrl != null)
                    existing.SetImage(entity.ImageUrl);

                if (entity.Description != null)
                    existing.SetDescription(entity.Description);

                if (entity.Url != null)
                    existing.SetUrl(entity.Url);

                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Project), entity.Id);
                await _repository.Projects.UpdateAsync(existing);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(Project), entity.Id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a project by its identifier.
        /// This method removes a project from the repository using its unique identifier.
        /// It does not return any value.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// It is typically used to remove projects that are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
             if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(Project));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(Project), id);
                var existing = await _repository.Projects.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(Project), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(Project), id);
                await _repository.Projects.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(Project), ex.Message);
                throw;
            }
        }
    }
}