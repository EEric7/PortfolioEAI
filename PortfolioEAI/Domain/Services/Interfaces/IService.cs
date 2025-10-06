namespace PortfolioEAI.Domain.Services.Interfaces
{
    public interface IService
    {
        public IProjectService ProjectService { get; set; }
        public ISkillService SkillService { get; set; }
        public IExperienceService ExperienceService { get; set; }
        public IAdminUserService AdminUserService { get; set; } 
    }
}