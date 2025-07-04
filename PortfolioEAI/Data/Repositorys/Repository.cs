using PortfolioEAI.Data.Repositorys.Interfaces;

namespace PortfolioEAI.Data.Repositorys
{
    public class Repository : IRepository
    {
        public IProjectRepository Projects { get; set; }
        public ISkillRepository Skills { get; set; }
        public IExperienceRepository Experiences { get; set; }
        public IAdminUserRepository AdminUsers { get; set; } 

        public Repository(IProjectRepository projects, ISkillRepository skills, IExperienceRepository experiences, IAdminUserRepository adminUsers)
        {
            Projects = projects;
            Skills = skills;
            Experiences = experiences;
            AdminUsers = adminUsers;
        }
    }
}