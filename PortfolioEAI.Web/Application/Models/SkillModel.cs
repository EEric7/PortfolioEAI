using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Application.Models
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
                Domain.Enums.SkillLevel.Beginner => " #67c29c",
                Domain.Enums.SkillLevel.Intermediate => " #0d6efd",
                Domain.Enums.SkillLevel.Advanced => " #7464a1",
                Domain.Enums.SkillLevel.Expert => " #6610f2",
                _ => "rgb(255, 255, 255)"
            };
        }
    }
}