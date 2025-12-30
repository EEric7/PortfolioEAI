using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Domain.Entities;
using PortfolioEAI.Web.Domain.Enums;

namespace PortfolioEAI.Web.Application.Mappings
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