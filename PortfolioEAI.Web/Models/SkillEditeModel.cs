

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillEditeModel : AMenuDashbordModel
    {
        [BindProperty]
        public SkillDto DTO { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem>? LevelOptions { get; }  = new List<SelectListItem>
        {
            new SelectListItem { Value = "None", Text = "None" },
            new SelectListItem { Value = "Beginner", Text = "Beginner" },
            new SelectListItem { Value = "Intermediate", Text = "Intermediate" },
            new SelectListItem { Value = "Advanced", Text = "Advanced" },
            new SelectListItem { Value = "Expert", Text = "Expert" }
        };

        [BindProperty]
        public List<SelectListItem>? CategoryOptions { get;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "None", Text = "None" },
            new SelectListItem { Value = "Frontend", Text = "Frontend" },
            new SelectListItem { Value = "Backend", Text = "Backend" },
            new SelectListItem { Value = "Fullstack", Text = "Fullstack" },
            new SelectListItem { Value = "DevOps", Text = "DevOps" },
            new SelectListItem { Value = "Framework", Text = "Framework" },
            new SelectListItem { Value = "Languages", Text = "Languages" },
            new SelectListItem { Value = "Mobile", Text = "Mobile" },
            new SelectListItem { Value = "Design", Text = "Design" }
        };
    }
}