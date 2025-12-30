namespace PortfolioEAI.Data.Repositorys.Interfaces
{
    public interface IRepository
    {
        public IProjectRepository Projects { get; protected set; }
        public ISkillRepository Skills { get; protected set; }
        public IExperienceRepository Experiences { get; protected set; }
        public IAdminUserRepository AdminUsers { get; protected set; }
    }
}