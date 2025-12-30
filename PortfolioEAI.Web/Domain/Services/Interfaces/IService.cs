namespace PortfolioEAI.Web.Domain.Services.Interfaces
{
    public interface IServices
    {
        public IProjectService ProjectService { get; set; }
        public ISkillService SkillService { get; set; }
        public IExperienceService ExperienceService { get; set; }
        public IAdminUserService AdminUserService { get; set; } 
    }
}