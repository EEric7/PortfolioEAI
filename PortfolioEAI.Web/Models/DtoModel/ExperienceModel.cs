using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ExperienceModel : ExperienceDto
    {
        public List<ProjectModel> ProjectsModel { get; set; } = new List<ProjectModel>();
        public ExperienceModel(ExperienceDto dto) : base(dto)
        {
            ProjectsModel = dto.Projects.Select(projectDto => new ProjectModel(projectDto)).ToList();
        }
    }
}