using PortfolioEAI.Data.Repositories.Interfaces;

namespace PortfolioEAI.Data.Repositories
{
    public class Repository : IRepository
    {
        public IProjectRepositorie Projects { get; set; }
        public ISkillRepository Skills { get; set; }
        public IExperienceRepository Experiences { get; set; }
        public IAdminUserRepository AdminUsers { get; set; } 

        public Repository(IProjectRepositorie projects, ISkillRepository skills, IExperienceRepository experiences, IAdminUserRepository adminUsers)
        {
            Projects = projects;
            Skills = skills;
            Experiences = experiences;
            AdminUsers = adminUsers;
        }
    }
}