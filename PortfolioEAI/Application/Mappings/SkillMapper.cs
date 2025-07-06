using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Application.Mappings
{
    public class SkillMapper
    {
        public static Skill ToEntity(SkillDto dto) => new Skill(
            dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            dto.Name ?? string.Empty,
            Enum.TryParse<SkillLevel>(dto.Level, out var level) ? level : SkillLevel.None,
            Enum.TryParse<SkillCategory>(dto.Category, out var category) ? category : SkillCategory.None
            );

        public static SkillDto ToDto(Skill dto) => new SkillDto
        {
            Id = dto.Id,
            Name = dto.Name ?? string.Empty,
            Level = dto.Level.ToString(),
            Category = dto.Category.ToString()
        };
    }
}