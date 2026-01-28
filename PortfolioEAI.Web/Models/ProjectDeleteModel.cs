using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ProjectDeleteModel : AMenuDashbordModel
    {
        [BindProperty]
        public ProjectDto DTO { get; set; } = default!;
        
    }
}