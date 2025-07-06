using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Data.Repositorys.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Services
{
    public class AdminUserService
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
        /// This method fetches all AdminUsers from the repository and maps them to DTOs.
        /// It returns an enumerable collection of <see cref="AdminUserDto"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdminUserDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(Messages.GetAllDTOInfo, nameof(AdminUserDto));
                var entitys = await _repository.AdminUsers.GetAllAsync();
                return entitys.Select(entity => AdminUserMapper.ToDto(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetAllDTOError, nameof(AdminUserDto), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="AdminUserDto"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUserDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError(Messages.GetNullIdDTOError, nameof(AdminUserDto));
                throw new ArgumentException(Messages.NullError, nameof(id));
            }
            try
            {
                _logger.LogInformation(Messages.GetEntityInfo, nameof(AdminUserDto), id);
                var entity = await _repository.AdminUsers.GetByIdAsync(id);
                if (entity == null) return null;
                return AdminUserMapper.ToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.GetDTOError, nameof(AdminUserDto), id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="AdminUserDto"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(AdminUserDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.AddNullError, nameof(AdminUserDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.AddDTOInfo, nameof(AdminUserDto), dto.Id);
                var project = AdminUserMapper.ToEntity(dto);
                foreach (var skillId in dto.Skills)
                {
                    var entity = await _repository.Skills.GetByIdAsync(skillId);
                    if (entity == null)
                    {
                        _logger.LogWarning(Messages.GetDTOError, nameof(Skill), skillId, Messages.NullError);
                        throw new ArgumentException(string.Format(Messages.AddNotFound, nameof(skillId), skillId));
                    }
                    project.AddSkill(entity);
                }
                await _repository.AdminUsers.AddAsync(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.AddDTOError, nameof(AdminUserDto), ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="AdminUserDto"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(AdminUserDto dto)
        {
            if (dto == null)
            {
                _logger.LogError(Messages.UpdateDTONullError, nameof(AdminUserDto));
                throw new ArgumentNullException(nameof(dto), Messages.NullError);
            }
            if (dto.Id == Guid.Empty)
            {
                _logger.LogError(Messages.UpdateNullIdError, nameof(AdminUserDto));
                throw new ArgumentException(Messages.NullError, nameof(dto.Id));
            }
            try
            {
                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(AdminUserDto), dto.Id);
                var existing = await _repository.AdminUsers.GetByIdAsync(dto.Id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.UpdateDTONotFound, nameof(AdminUserDto), dto.Id);
                    return;
                }
                if (dto.UserName != null)
                    existing.SetUsername(dto.UserName);

                if (dto.Password != null)
                    existing.SetPassword(dto.Password);

                if (dto.Email != null)
                    existing.SetEmail(dto.Email);

                _logger.LogInformation(Messages.UpdateDTOInfo, nameof(AdminUserDto), dto.Id);
                await _repository.AdminUsers.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.UpdateDTOError, nameof(AdminUserDto), dto.Id, ex.Message);
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
             if (id == Guid.Empty)
            {
                _logger.LogError(Messages.DeleteDTOEmptyIdError, nameof(AdminUserDto));
                throw new ArgumentException(nameof(id), Messages.NullError);
            }
            try
            {
                _logger.LogInformation(Messages.DeleteAttemptDTOInfo, nameof(AdminUserDto), id);
                var existing = await _repository.AdminUsers.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning(Messages.DeleteDTONotFound, nameof(AdminUserDto), id);
                    return;
                }
                _logger.LogInformation(Messages.DeleteDTOInfo, nameof(AdminUserDto), id);
                await _repository.AdminUsers.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Messages.DeleteDTOError, nameof(AdminUserDto), ex.Message);
                throw;
            }
        }
    }
}