

using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ProjectModel : ProjectDto
    {
        public ProjectModel(ProjectDto dto) : base(dto)
        {
        }
    }
}