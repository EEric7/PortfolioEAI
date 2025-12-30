using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Application.Models
{
    public class ProjectModel : ProjectDto
    {
        public ProjectModel(ProjectDto dto) : base(dto)
        {
        }
    }
}