namespace PortfolioEAI.Application.DTOs
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
        public string? Level { get; set; }

        /// <summary>
        /// Category of the skill, such as frontend, backend, fullstack, devops, etc.
        /// </summary>
        public string? Category { get; set; }

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