using PortfolioEAI.Web.Domain.Enums;

namespace PortfolioEAI.Web.Application.DTOs
{
    public class SkillDto
    {
        /// <summary>
        /// Unique identifier for the skill.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the skill.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Level of proficiency in the skill.
        /// </summary>
        public SkillLevel? Level { get; set; }

        /// <summary>
        /// Category of the skill, such as frontend, backend, fullstack, devops, etc.
        /// </summary>
        public SkillCategory? Category { get; set; }

        public SkillDto() {}

        public SkillDto(SkillDto skillDto)
        {
            Id = skillDto.Id;
            Name = skillDto.Name;
            Level = skillDto.Level;
            Category = skillDto.Category;
        }
    }
}