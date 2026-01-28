using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ExperienceDeleteModel : AMenuDashbordModel
    {
        [BindProperty]
        public ExperienceDto Experience { get; set; } = default!;
    }
}