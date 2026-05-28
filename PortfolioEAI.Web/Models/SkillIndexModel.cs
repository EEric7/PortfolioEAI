
using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Models
{
    public class SkillIndexModel : AMenuModel
    {
        [BindProperty]
        public List<SkillModel> SkillModels { get;set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = string.Empty;
    }
}