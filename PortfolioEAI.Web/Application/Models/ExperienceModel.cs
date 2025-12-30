using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Application.Models
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