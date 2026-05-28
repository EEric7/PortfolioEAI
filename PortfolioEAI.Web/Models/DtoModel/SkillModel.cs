using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillModel
    {
        [BindProperty]
        public SkillDto? DTO { get; set; } = default;

        [BindProperty]
        public string? LevelBackgroundColor { get; set; } = default;

        public SkillModel(SkillDto value)
        {
            DTO = value;
            SetLevelBackgroundColor();
        }
        
        public SkillModel() { }

        private void SetLevelBackgroundColor()
        {
            LevelBackgroundColor = DTO?.Level switch
            {
                "Beginner" => " #67c29c",
                "Intermediate" => " #0d6efd",
                "Advanced" => " #7464a1",
                "Expert" => " #6610f2",
                _ => "rgb(255, 255, 255)"
            };
        }
    }
}