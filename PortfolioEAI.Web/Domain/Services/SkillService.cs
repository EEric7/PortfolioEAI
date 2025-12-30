using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Services.Interfaces;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Domain.Services
{
    public class SkillService : ISkillService
    {
        /// <summary>
        /// Logger for logging information, warnings, and errors related to project operations.
        /// This logger is used to track the flow of operations, debug issues, and provide insights
        /// into the behavior of the project service.
        /// It is typically injected via dependency injection in ASP.NET Core applications.
        /// </summary>
        private readonly ILogger<SkillService> _logger;

        /// <summary>
        /// Service for managing Skills.
        /// </summary>
        private readonly IRepository _repository;

        public SkillService(ILogger<SkillService> logger, IRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), Messages.NullError);
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), Messages.NullError);
        }


        /// <summary>
        /// Retrieves all Skills.
        /// This method fetches all Skills from the repository and maps them to DTOs.
        /// It returns an enumerable collection of <see cref="Skill"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(Skill));
                return await _repository.Skills.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(Skill), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="Skill"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Skill?> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(Skill), id);
                return await _repository.Skills.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(Skill), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="Skill"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the entity is null.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(Skill entity)
        {
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(Skill), entity.Id);
                await _repository.Skills.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(Skill), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="Skill"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the entity. It throws an <see cref="ArgumentNullException"/> if the entity is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(Skill entity)
        {
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Skill), entity.Id);
                var existing = await _repository.Skills.GetByIdAsync(entity.Id);

                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(Skill), entity.Id);
                    return;
                }

                if (entity.Name != existing.Name)
                    existing.SetName(entity.Name);

                if (entity.Level != existing.Level)
                    existing.SetLevel(entity.Level);

                if (entity.Category != existing.Category)
                    existing.SetCategory(entity.Category);

                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(Skill), entity.Id);
                await _repository.Skills.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(Skill), entity.Id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a project by its identifier.
        /// This method removes a project from the repository using its unique identifier.
        /// It does not return any value.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// It is typically used to remove Skills that are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(Skill), id);
                var existing = await _repository.Skills.GetByIdAsync(id);

                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(Skill), id);
                    return;
                }

                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(Skill), id);
                await _repository.Skills.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(Skill), ex.Message);
                throw;
            }
        }
    }
}