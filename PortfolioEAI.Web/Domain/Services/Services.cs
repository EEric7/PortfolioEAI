using PortfolioEAI.Web.Domain.Services.Interfaces;

namespace PortfolioEAI.Web.Domain.Services
{
    public class Services : IServices
    {
        public IProjectService ProjectService { get; set; }
        public ISkillService SkillService { get; set; }
        public IExperienceService ExperienceService { get; set; }
        public IAdminUserService AdminUserService { get; set; }

        public Services(IProjectService projectService, ISkillService skillService, IExperienceService experienceService, IAdminUserService adminUserService)
        {
            ProjectService = projectService;
            SkillService = skillService;
            ExperienceService = experienceService;
            AdminUserService = adminUserService;
        }
    }
}