using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class IndexExperienceModel : AMenuDashbordModel
    {
        [BindProperty]
        public IList<ExperienceDto> DTOs { get;set; } = new List<ExperienceDto>();

        [BindProperty]
        public Guid focusId { get; set; } = Guid.Empty;
    }
}