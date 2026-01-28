
using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillIndexModel : AMenuModel
    {
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = string.Empty;

        [BindProperty]
        public IList<SkillDto> DTOs { get;set; } = default!;
    }
}