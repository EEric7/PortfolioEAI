using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillModel : SkillDto
    {
        public string? LevelBackgroundColor { get; set; }

        public SkillModel(SkillDto skillDto) : base(skillDto)
        {
            SetLevelBackgroundColor();
        }

        public void SetLevelBackgroundColor()
        {
            LevelBackgroundColor = Level switch
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