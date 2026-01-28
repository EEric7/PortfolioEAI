using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ExperienceCreateModel : AMenuDashbordModel
    {
        [BindProperty]
        public Guid UserId { get; set; }

        [BindProperty]
        public ExperienceDto Experience { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? PhotoFile { get; set; }
    }
}