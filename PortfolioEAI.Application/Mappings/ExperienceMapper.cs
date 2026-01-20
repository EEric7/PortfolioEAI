using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    internal class ExperienceMapper
    {
        public static ExperienceDto ToDto(Experience exp) => new ExperienceDto
        {
            Id = exp.Id,
            Company = exp.Company,
            Position = exp.Position,
            Description = exp.Description ?? string.Empty,
        };
    }
}