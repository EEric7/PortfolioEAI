using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Application.Mappings
{
    internal class SkillMapper
    {
        public static Skill ToEntity(SkillDto dto) => new Skill(
            dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            dto.Name ?? string.Empty,
            Enum.TryParse<SkillLevel>(dto.Level?.ToString(), out var level) ? level : SkillLevel.None,
            Enum.TryParse<SkillCategory>(dto.Category?.ToString(), out var category) ? category : SkillCategory.None
            );

        public static SkillDto ToDto(Skill entity) => new SkillDto
        {
            Id = entity.Id,
            Name = entity.Name ?? string.Empty,
            Level = entity.Level,
            Category = entity.Category
        };
    }
}