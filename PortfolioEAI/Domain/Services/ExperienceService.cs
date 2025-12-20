using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Services.Interfaces;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Domain.Services
{
    public class ExperienceService : IExperienceService
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<ExperienceService> _logger;

        /// <summary>
        /// Service for managing Experiences.
        /// </summary>
        private readonly IRepository _repository;

        public ExperienceService(ILogger<ExperienceService> logger, IRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.NullError);
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), Messages.NullError);
        }
        
        /// <summary>
        /// Retrieves all Experiences.
        /// This method fetches all Experiences from the repository and maps them to DTOs.
        /// It returns an enumerable collection of <see cref="ExperienceDto"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Experience>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(Experience));
                return await _repository.Experiences.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(Experience), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="Experience"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Experience?> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(Experience), id);
                return await _repository.Experiences.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(Experience), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="Experience"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(Experience entity)
        {
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(Experience), entity.Id);
                await _repository.Experiences.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(Experience), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="Experience"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(Experience entity)
        {
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Experience), entity.Id);
                var existing = await _repository.Experiences.GetByIdAsync(entity.Id);

                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(Experience), entity.Id);
                    return;
                }

                if (entity.Company != existing.Company)
                    existing.SetCompany(entity.Company);

                if (entity.Position != existing.Position)
                    existing.SetPosition(entity.Position);

                if (entity.StartDate != existing.StartDate)
                    existing.SetStartDate(entity.StartDate);
                
                if (entity.EndDate != existing.EndDate)
                    existing.SetEndDate(entity.EndDate);

                if (entity.Description != existing.Description)
                    existing.SetDescription(entity.Description);
                
                if (entity.ImageUrl != existing.ImageUrl)
                    existing.SetImageUrl(entity.ImageUrl);
                
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Experience), entity.Id);
                await _repository.Experiences.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(Experience), entity.Id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a project by its identifier.
        /// This method removes a project from the repository using its unique identifier.
        /// It does not return any value.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// It is typically used to remove Experiences that are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(Experience), id);
                var existing = await _repository.Experiences.GetByIdAsync(id);

                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(Experience), id);
                    return;
                }
                
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(Experience), id);
                await _repository.Experiences.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(Experience), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the Experience Id by Project Id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Guid> GetExperienceIdByProjectIdAsync(Guid projectId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve Experience ID for Project ID: {ProjectId}", projectId);
                var project = (await _repository.Experiences.GetAllAsync()).FirstOrDefault(x => x.Projects.Any(p => p.Id == projectId));

                if (project == null)
                {
                    _logger.LogWarning("Project with ID: {ProjectId} not found.", projectId);
                    return Guid.Empty;
                }

                _logger.LogInformation("Successfully retrieved Experience ID for Project ID: {ProjectId}", projectId);
                return project.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Experience ID for Project ID: {ProjectId}. Error: {ErrorMessage}", projectId, ex.Message);
                throw;
            }
        }
    }
}