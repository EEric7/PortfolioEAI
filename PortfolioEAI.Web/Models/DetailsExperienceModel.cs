using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class DetailsExperienceModel : AMenuDashbordModel
    {
        [BindProperty]
        public ExperienceDto DTO { get; set; } = default!;
    }
}