using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Models
{
    public class ProjectModel : ProjectDto
    {
        public ProjectModel(ProjectDto dto) : base(dto)
        {
        }
    }
}