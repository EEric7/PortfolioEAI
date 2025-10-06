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
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(Experience));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
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
            if (entity == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(Experience));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }
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
            if (entity == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(Experience));
                throw new ArgumentNullException(nameof(entity), Messages.NullError);
            }
            if (entity.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(Experience));
                throw new ArgumentException(Messages.NullError, nameof(entity.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Experience), entity.Id);
                var existing = await _repository.Experiences.GetByIdAsync(entity.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(Experience), entity.Id);
                    return;
                }
                if (entity.Company != null)
                    existing.SetCompany(entity.Company);

                if (entity.Position != null)
                    existing.SetPosition(entity.Position);

                if (entity.StartDate != default)
                    existing.SetStartDate(entity.StartDate);
                
                if (entity.EndDate != default)
                    existing.SetStartDate(entity.EndDate);

                if (entity.Description != null)
                    existing.SetDescription(entity.Description);
                
                if (entity.ImageUrl != null)
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
             if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(Experience));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
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
    }
}