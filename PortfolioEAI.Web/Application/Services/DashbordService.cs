using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Domain.Services.Interfaces;

namespace PortfolioEAI.Web.Application.Services.Interfaces
{
    public class DashbordService : IDashbordService
    {
        private readonly ILogger<HomepageService> _logger;
        private readonly IServices _serviceDomain;

        public DashbordService(ILogger<HomepageService> logger, IServices serviceDomain)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "");
            _serviceDomain = serviceDomain ?? throw new ArgumentNullException(nameof(serviceDomain), "");
        }

        #region Check DTO Methods
        public async Task<bool> AdminUserExistsAsync(AdminUserDto dto)
        {
            var adminUsers = await _serviceDomain.AdminUserService.GetAllAsync();
            var existingAdminUser = adminUsers.FirstOrDefault(e => Mappings.AdminUserMapper.ToDto(e).UserName == dto.UserName && Mappings.AdminUserMapper.ToDto(e).Email == dto.Email);
            return existingAdminUser is not null;
        }
        public async Task<bool> AdminUserExistsAsync(Guid id)
        {
            var adminUser = await _serviceDomain.AdminUserService.GetByIdAsync(id);
            return adminUser is not null;
        }
        public async Task<bool> ExperienceExistsAsync(ExperienceDto dto)
        {
            var experiences = await _serviceDomain.ExperienceService.GetAllAsync();
            var existingExperience = experiences.FirstOrDefault(e => Mappings.ExperienceMapper.ToDto(e).Title == dto.Title && Mappings.ExperienceMapper.ToDto(e).Company == dto.Company);
            return existingExperience is not null;
        }
        public async Task<bool> ExperienceExistsAsync(Guid id)
        {
            var experience = await _serviceDomain.ExperienceService.GetByIdAsync(id);
            return experience is not null;
        }
        public async Task<bool> ProjectExistsAsync(ProjectDto dto)
        {
            var projects = await _serviceDomain.ProjectService.GetAllAsync();
            var existingProject = projects.FirstOrDefault(e => Mappings.ProjectMapper.ToDto(e).Title == dto.Title && Mappings.ProjectMapper.ToDto(e).Description == dto.Description);
            return existingProject is not null;
        }
        public async Task<bool> ProjectExistsAsync(Guid id)
        {
            var project = await _serviceDomain.ProjectService.GetByIdAsync(id);
            return project is not null;
        }
        public async Task<bool> SkillExistsAsync(SkillDto dto)
        {
            var skills = await _serviceDomain.SkillService.GetAllAsync();
            var existingSkill = skills.FirstOrDefault(e => Mappings.SkillMapper.ToDto(e).Name == dto.Name && Mappings.SkillMapper.ToDto(e).Category == dto.Category);
            return existingSkill is not null;
        }
        public async Task<bool> SkillExistsAsync(Guid id)
        {
            var skill = await _serviceDomain.SkillService.GetByIdAsync(id);
            return skill is not null;
        }
        #endregion

        #region Admin User Methods
        public async Task CreateAdminUserAsync(AdminUserDto adminUserDto)
        {
            try
            {
                _logger.LogInformation("Creating a new admin user with username: {UserName}", adminUserDto.UserName);
                await _serviceDomain.AdminUserService.AddAsync(Mappings.AdminUserMapper.ToEntity(adminUserDto));
                _logger.LogInformation("Successfully created admin user with username: {UserName}", adminUserDto.UserName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating admin user with username: {UserName}. Error: {ErrorMessage}", adminUserDto.UserName, ex.Message);
                throw;
            }
        }
        public async Task UpdateAdminUserAsync(AdminUserDto adminUser)
        {
            try
            {
                _logger.LogInformation("Updating admin user with ID: {AdminUserId}", adminUser.Id);
                if (!await AdminUserExistsAsync(adminUser))
                {
                    _logger.LogWarning("Admin user with ID: {AdminUserId} not found for update.", adminUser.Id);
                    throw new InvalidOperationException($"Admin user with ID {adminUser.Id} does not exist.");
                }

                await _serviceDomain.AdminUserService.UpdateAsync(Mappings.AdminUserMapper.ToEntity(adminUser));
                _logger.LogInformation("Successfully updated admin user with ID: {AdminUserId}", adminUser.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating admin user with ID: {AdminUserId}. Error: {ErrorMessage}", adminUser.Id, ex.Message);
                throw;
            }
        }
        public async Task<AdminUserDto?> DeleteAdminUserByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting admin user with ID: {AdminUserId}", id);
                var adminUser = await _serviceDomain.AdminUserService.GetByIdAsync(id);
                if (adminUser is null)
                {
                    _logger.LogWarning("Admin user with ID: {AdminUserId} not found for deletion.", id);
                    return null;
                }

                await _serviceDomain.AdminUserService.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted admin user with ID: {AdminUserId}", id);
                return Mappings.AdminUserMapper.ToDto(adminUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting admin user with ID: {AdminUserId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task<AdminUserDto?> GetAdminUserByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving admin user with ID: {AdminUserId}", id);
                var adminUser = await _serviceDomain.AdminUserService.GetByIdAsync(id);
                if (adminUser is null)
                    _logger.LogWarning("Admin user with ID: {AdminUserId} not found.", id);
                else
                    _logger.LogInformation("Successfully retrieved admin user with ID: {AdminUserId}", id);

                return adminUser is null ? null : Mappings.AdminUserMapper.ToDto(adminUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving admin user with ID: {AdminUserId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task<IList<AdminUserDto>> GetAllAdminUsersAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all admin users.");
                var adminUsers = await _serviceDomain.AdminUserService.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} admin users.", adminUsers.Count());
                return adminUsers.Select(Mappings.AdminUserMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all admin users. Error: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        #endregion

        #region Experiences User Methods
        public async Task<IList<ExperienceDto>> GetAllExperiencesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all experiences.");
                var experiences = await _serviceDomain.ExperienceService.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} experiences.", experiences.Count());
                return experiences.Select(Mappings.ExperienceMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all experiences. Error: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        public async Task<ExperienceDto?> GetExperienceByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving experience with ID: {ExperienceId}", id);
                var experience = await _serviceDomain.ExperienceService.GetByIdAsync(id);

                if (experience is null)
                    _logger.LogWarning("Experience with ID: {ExperienceId} not found.", id);
                else
                    _logger.LogInformation("Successfully retrieved experience with ID: {ExperienceId}", id);

                return experience is null ? null : Mappings.ExperienceMapper.ToDto(experience);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving experience with ID: {ExperienceId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task UpdateExperienceAsync(ExperienceDto experience)
        {
            try
            {
                _logger.LogInformation("Updating experience with ID: {ExperienceId}", experience.Id);
                if (!await ExperienceExistsAsync(experience))
                {
                    _logger.LogWarning("Experience with ID: {ExperienceId} not found for update.", experience.Id);
                    throw new InvalidOperationException($"Experience with ID {experience.Id} does not exist.");
                }
                await _serviceDomain.ExperienceService.UpdateAsync(Mappings.ExperienceMapper.ToEntity(experience));
                _logger.LogInformation("Successfully updated experience with ID: {ExperienceId}", experience.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating experience with ID: {ExperienceId}. Error: {ErrorMessage}", experience.Id, ex.Message);
                throw;
            }
        }
        public async Task DeleteExperienceAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting experience with ID: {ExperienceId}", id);
                if (!await ExperienceExistsAsync(id))
                {
                    _logger.LogWarning("Experience with ID: {ExperienceId} not found for deletion.", id);
                    throw new InvalidOperationException($"Experience with ID {id} does not exist.");
                }
                await _serviceDomain.ExperienceService.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted experience with ID: {ExperienceId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting experience with ID: {ExperienceId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task AddExperienceAsync(ExperienceDto dto)
        {
            try
            {
                _logger.LogInformation("Adding a new experience with title: {Title}", dto.Title);
                await _serviceDomain.ExperienceService.AddAsync(Mappings.ExperienceMapper.ToEntity(dto));
                _logger.LogInformation("Successfully added experience with title: {Title}", dto.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding experience with title: {Title}. Error: {ErrorMessage}", dto.Title, ex.Message);
                throw;
            }
        }
        #endregion

        #region Projects User Methods
        public async Task<IList<ProjectDto>> GetAllProjectsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all projects.");
                var projects = await _serviceDomain.ProjectService.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} projects.", projects.Count());
                return projects.Select(Mappings.ProjectMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all projects. Error: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving project with ID: {ProjectId}", id);
                var project = await _serviceDomain.ProjectService.GetByIdAsync(id);

                if (project is null)
                {
                    _logger.LogWarning("Project with ID: {ProjectId} not found.", id);
                    return null;
                }

                _logger.LogInformation("Successfully retrieved project with ID: {ProjectId}", id);
                return Mappings.ProjectMapper.ToDto(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving project with ID: {ProjectId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }

        public async Task<Guid> GetExperienceIdByProjectIdAsync(Guid projectId)
        {
            try
            {
                _logger.LogInformation("Retrieving experience ID for project with ID: {ProjectId}", projectId);
                Guid experienceId = await _serviceDomain.ExperienceService.GetExperienceIdByProjectIdAsync(projectId);

                if (experienceId == Guid.Empty)
                {
                    _logger.LogWarning("No experience found for project with ID: {ProjectId}", projectId);
                    return Guid.Empty;
                }
    
                _logger.LogInformation("Successfully retrieved experience ID for project with ID: {ProjectId}", projectId);
                return experienceId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving experience ID for project with ID: {ProjectId}. Error: {ErrorMessage}", projectId, ex.Message);
                throw;
            }
        } 

        public async Task UpdateProjectAsync(ProjectDto project)
        {
            try
            {
                _logger.LogInformation("Updating project with ID: {ProjectId}", project.Id);

                if (!await ProjectExistsAsync(project.Id))
                {
                    _logger.LogWarning("Project with ID: {ProjectId} not found.", project.Id);
                    return;
                }

                await _serviceDomain.ProjectService.UpdateAsync(Mappings.ProjectMapper.ToEntity(project));
                _logger.LogInformation("Successfully updated project with ID: {ProjectId}", project.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating project with ID: {ProjectId}. Error: {ErrorMessage}", project.Id, ex.Message);
                throw;
            }
        }
        public async Task DeleteProjectAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting project with ID: {ProjectId}", id);
                var existingProject = _serviceDomain.ProjectService.GetByIdAsync(id).Result;

                if (!await ProjectExistsAsync(id))
                {
                    _logger.LogWarning("Project with ID: {ProjectId} not found.", id);
                    return;
                }

                await _serviceDomain.ProjectService.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted project with ID: {ProjectId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting project with ID: {ProjectId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task AddProjectAsync(ProjectDto dto)
        {
            try
            {
                _logger.LogInformation("Adding a new project with title: {Title}", dto.Title);
                await _serviceDomain.ProjectService.AddAsync(Mappings.ProjectMapper.ToEntity(dto));
                _logger.LogInformation("Successfully added project with title: {Title}", dto.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding project with title: {Title}. Error: {ErrorMessage}", dto.Title, ex.Message);
                throw;
            }
        }
        #endregion

        #region Skills User Methods
        public async Task<IList<SkillDto>> GetAllSkillsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all skills.");
                var skills = await _serviceDomain.SkillService.GetAllAsync();

                _logger.LogInformation("Successfully retrieved {Count} skills.", skills.Count());
                return skills.Select(Mappings.SkillMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all skills. Error: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        public async Task<SkillDto?> GetSkillByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving skill with ID: {SkillId}", id);
                var skill = await _serviceDomain.SkillService.GetByIdAsync(id);

                if (skill is null)
                {
                    _logger.LogWarning("Skill with ID: {SkillId} not found.", id);
                    return null;
                }

                _logger.LogInformation("Successfully retrieved skill with ID: {SkillId}", id);
                return Mappings.SkillMapper.ToDto(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving skill with ID: {SkillId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task UpdateSkillAsync(SkillDto skill)
        {
            try
            {
                _logger.LogInformation("Updating skill with ID: {SkillId}", skill.Id);
                if (!await SkillExistsAsync(skill.Id))
                {
                    _logger.LogWarning("Skill with ID: {SkillId} not found.", skill.Id);
                    return;
                }

                await _serviceDomain.SkillService.UpdateAsync(Mappings.SkillMapper.ToEntity(skill));
                _logger.LogInformation("Successfully updated skill with ID: {SkillId}", skill.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating skill with ID: {SkillId}. Error: {ErrorMessage}", skill.Id, ex.Message);
                throw;
            }
        }
        public async Task DeleteSkillAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting skill with ID: {SkillId}", id);
                if (!await SkillExistsAsync(id))
                {
                    _logger.LogWarning("Skill with ID: {SkillId} not found.", id);
                    return;
                }

                await _serviceDomain.SkillService.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted skill with ID: {SkillId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting skill with ID: {SkillId}. Error: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }
        public async Task AddSkillAsync(SkillDto dto)
        {
            try
            {
                _logger.LogInformation("Adding a new skill with name: {Name}", dto.Name);
                await _serviceDomain.SkillService.AddAsync(Mappings.SkillMapper.ToEntity(dto));
                _logger.LogInformation("Successfully added skill with name: {Name}", dto.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding skill with name: {Name}. Error: {ErrorMessage}", dto.Name, ex.Message);
                throw;
            }
        }
        #endregion
    }
}