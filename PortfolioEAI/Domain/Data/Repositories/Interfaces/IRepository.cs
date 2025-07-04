using PortfolioEAI.Data.Repositories.Interfaces;

namespace PortfolioEAI.Data.Repositories
{
    public interface IRepository
    {
        public IProjectRepositorie Projects { get; protected set; }
        public ISkillRepository Skills { get; protected set; }
        public IExperienceRepository Experiences { get; protected set; }
        public IAdminUserRepository AdminUsers { get; protected set; }
    }
}