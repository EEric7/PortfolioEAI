using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ProjectCreateModel : AMenuDashbordModel
    {
        [BindProperty]
        public ProjectDto? DTO { get; set; } = default!;

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        [BindProperty]
        public ExperienceDto? ExperienceDTO { get; set; } = default!;
        
    }
}