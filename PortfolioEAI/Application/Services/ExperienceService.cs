using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Services
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
        public async Task<IEnumerable<ExperienceDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(ExperienceDto));
                var entitys = await _repository.Experiences.GetAllAsync();
                return entitys.Select(entity => ExperienceMapper.ToDto(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(ExperienceDto), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="ExperienceDto"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExperienceDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(ExperienceDto));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(ExperienceDto), id);
                var entity = await _repository.Experiences.GetByIdAsync(id);
                if (entity == null) return null;
                return ExperienceMapper.ToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(ExperienceDto), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="ExperienceDto"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(ExperienceDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(ExperienceDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(ExperienceDto), dto.Id);
                var entity = ExperienceMapper.ToEntity(dto);
                if (dto.Projects != null && dto.Projects.Any())
                {
                    foreach (var projectId in dto.Projects)
                    {
                        var project = await _repository.Projects.GetByIdAsync(projectId);
                        if (project == null)
                        {
                            _logger.LogWarning(Messages.GetDTOError, nameof(Project), projectId, Messages.NullError);
                            throw new ArgumentException(string.Format(Messages.AddNotFound, nameof(Project), projectId, nameof(Experience), entity.Id));
                        }
                        entity.Projects.Add(project);
                    }
                    await _repository.Experiences.AddAsync(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(ExperienceDto), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="ExperienceDto"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(ExperienceDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(ExperienceDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            if (dto.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(ExperienceDto));
                throw new ArgumentException(Messages.NullError, nameof(dto.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(ExperienceDto), dto.Id);
                var existing = await _repository.Experiences.GetByIdAsync(dto.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(ExperienceDto), dto.Id);
                    return;
                }
                if (dto.Company != null)
                    existing.SetCompany(dto.Company);

                if (dto.Position != null)
                    existing.SetPosition(dto.Position);

                if (dto.StartDate != default)
                    existing.SetStartDate(dto.StartDate);
                
                if (dto.EndDate != default)
                    existing.SetStartDate(dto.EndDate);

                if (dto.Description != null)
                    existing.SetDescription(dto.Description);
                
                if (dto.ImageUrl != null)
                    existing.SetImageUrl(dto.ImageUrl);
                
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(ExperienceDto), dto.Id);
                await _repository.Experiences.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(ExperienceDto), dto.Id, ex.Message);
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
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(ExperienceDto));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(ExperienceDto), id);
                var existing = await _repository.Experiences.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(ExperienceDto), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(ExperienceDto), id);
                await _repository.Experiences.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(ExperienceDto), ex.Message);
                throw;
            }
        }
    }
}