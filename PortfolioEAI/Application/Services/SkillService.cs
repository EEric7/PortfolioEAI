using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Services
{
    public class SkillService
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
        /// It returns an enumerable collection of <see cref="SkillDto"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SkillDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(SkillDto));
                var entitys = await _repository.Skills.GetAllAsync();
                return entitys.Select(entity => SkillMapper.ToDto(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(SkillDto), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="SkillDto"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SkillDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(SkillDto));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(SkillDto), id);
                var entity = await _repository.Skills.GetByIdAsync(id);
                if (entity == null) return null;
                return SkillMapper.ToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(SkillDto), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="SkillDto"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(SkillDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(SkillDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(SkillDto), dto.Id);
                var entity = SkillMapper.ToEntity(dto);
                await _repository.Skills.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(SkillDto), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="SkillDto"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(SkillDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(SkillDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            if (dto.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(SkillDto));
                throw new ArgumentException(Messages.NullError, nameof(dto.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(SkillDto), dto.Id);
                var existing = await _repository.Skills.GetByIdAsync(dto.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(SkillDto), dto.Id);
                    return;
                }
                if (dto.Name != null)
                    existing.SetName(dto.Name);

                if (dto.Level != null)
                    existing.SetLevel(dto.Level);

                if (dto.Category != default)
                    existing.SetCategory(dto.Category);
                
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(SkillDto), dto.Id);
                await _repository.Skills.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(SkillDto), dto.Id, ex.Message);
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
             if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(SkillDto));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(SkillDto), id);
                var existing = await _repository.Skills.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(SkillDto), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(SkillDto), id);
                await _repository.Skills.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(SkillDto), ex.Message);
                throw;
            }
        }
    }
}