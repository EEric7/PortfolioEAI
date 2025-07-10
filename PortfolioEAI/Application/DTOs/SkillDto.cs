using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioEAI.Domain.Enums;

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
        public SkillLevel? Level { get; set; }

        /// <summary>
        /// Category of the skill, such as frontend, backend, fullstack, devops, etc.
        /// </summary>
        public SkillCategory? Category { get; set; }
    }
}