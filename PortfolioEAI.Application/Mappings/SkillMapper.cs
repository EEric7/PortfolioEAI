using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    internal class SkillMapper
    {
        public static SkillDto ToDto(Skill entity) 
        {
            return new SkillDto{
            Id = entity.Id,
            Name = entity.Name ?? string.Empty,
            Level = entity.Level.ToString(),
            Category = entity.Category.ToString()
            };
        } 
    }
}