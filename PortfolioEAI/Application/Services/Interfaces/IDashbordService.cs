using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Services.Interfaces
{
    public interface IDashbordService
    {
        #region Check DTO Methods
        Task<bool> AdminUserExistsAsync(AdminUserDto dto);
        Task<bool> AdminUserExistsAsync(Guid id);
        Task<bool> ExperienceExistsAsync(ExperienceDto dto);
        Task<bool> ExperienceExistsAsync(Guid id);
        Task<bool> ProjectExistsAsync(ProjectDto dto);
        Task<bool> ProjectExistsAsync(Guid id);
        Task<bool> SkillExistsAsync(SkillDto dto);
        Task<bool> SkillExistsAsync(Guid id);
        #endregion

        #region Admin User Methods
        Task CreateAdminUserAsync(AdminUserDto adminUser);
        Task<AdminUserDto?> GetAdminUserByIdAsync(Guid id);
        Task<IList<AdminUserDto>> GetAllAdminUsersAsync();
        Task<AdminUserDto?> DeleteAdminUserByIdAsync(Guid id);
        Task UpdateAdminUserAsync(AdminUserDto adminUser);
        #endregion

        #region Experience Methods
        Task<IList<ExperienceDto>> GetAllExperiencesAsync();
        Task<ExperienceDto?> GetExperienceByIdAsync(Guid id);
        Task UpdateExperienceAsync(ExperienceDto experience);
        Task DeleteExperienceAsync(Guid id);
        Task AddExperienceAsync(ExperienceDto experience);
        #endregion

        #region Project Methods
        Task<IList<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(Guid id);
        Task DeleteProjectAsync(Guid id);
        Task UpdateProjectAsync(ProjectDto project);
        Task AddProjectAsync(ProjectDto project);
        #endregion

        #region Skill Methods
        Task<IList<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto?> GetSkillByIdAsync(Guid id);
        Task DeleteSkillAsync(Guid id);
        Task UpdateSkillAsync(SkillDto skill);
        Task AddSkillAsync(SkillDto skill);
        #endregion
    }
}