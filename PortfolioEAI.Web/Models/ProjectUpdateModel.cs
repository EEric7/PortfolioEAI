using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ProjectUpdateModel : AMenuDashbordModel
    {
        [BindProperty]
        public ProjectDto DTO { get; set; } = default!;

        [BindProperty]
        public Guid ExperienceId { get; set; } = default!;
    }
}