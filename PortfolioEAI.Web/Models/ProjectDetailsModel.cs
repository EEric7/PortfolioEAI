
using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ProjectDetailsModel : AMenuDashbordModel
    {
        [BindProperty]
        public Guid ExperienceId { get; set; } = default!;

        [BindProperty]
        public ProjectDto DTO { get; set; } = default!;
    }
}