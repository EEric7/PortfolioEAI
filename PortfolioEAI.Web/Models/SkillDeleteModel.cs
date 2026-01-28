using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillDeleteModel : AMenuDashbordModel
    {
        [BindProperty]
        public SkillDto DTO { get; set; } = default!;
    }
}