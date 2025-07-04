using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Services
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
        /// It returns an enumerable collection of <see cref="ProjectDto"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(ProjectDto));
                var projects = await _repository.Projects.GetAllAsync();
                return projects.Select(p => ProjectMapper.ToDto(p));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(ProjectDto), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="ProjectDto"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(AdminUser));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(ProjectDto), id);
                var p = await _repository.Projects.GetByIdAsync(id);
                if (p == null) return null;
                return ProjectMapper.ToDto(p);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(ProjectDto), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="ProjectDto"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(ProjectDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(ProjectDto), dto.Title);
                var project = ProjectMapper.ToEntity(dto);
                await _repository.Projects.AddAsync(project);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(ProjectDto), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="ProjectDto"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(ProjectDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            if (dto.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(ProjectDto));
                throw new ArgumentException(Messages.NullError, nameof(dto.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(ProjectDto), dto.Id);
                var existing = await _repository.Projects.GetByIdAsync(dto.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(ProjectDto), dto.Id);
                    return;
                }
                if (dto.Title != null)
                    existing.SetTitle(dto.Title);

                if (dto.ImageUrl != null)
                    existing.SetImage(dto.ImageUrl);

                if (dto.Description != null)
                    existing.SetDescription(dto.Description);

                if (dto.Url != null)
                    existing.SetUrl(dto.Url);

                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(ProjectDto), dto.Id);
                await _repository.Projects.UpdateAsync(existing);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(ProjectDto), dto.Id, ex.Message);
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
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(ProjectDto));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(ProjectDto), id);
                var existing = await _repository.Projects.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(ProjectDto), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(ProjectDto), id);
                await _repository.Projects.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(ProjectDto), ex.Message);
                throw;
            }
        }
    }
}