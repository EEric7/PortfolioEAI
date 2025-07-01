using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Services
{
    public class Services : IServices
    {
        public IGenericServices<ProjectDto> Projects { get; }

        public Services(IGenericServices<ProjectDto> projectsService)
        {
            Projects = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
        }
    }
}