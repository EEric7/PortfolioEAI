using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    internal class ExperienceMapper
    {
        public static Experience ToEntity(ExperienceDto dto)
        {
            var experience = new Experience(
                dto.Id,
                dto.Company ?? string.Empty,
                dto.Position ?? string.Empty,
                dto.StartDate ?? DateOnly.FromDateTime(DateTime.Now),
                dto.EndDate ?? DateOnly.FromDateTime(DateTime.Now),
                dto.Description ?? string.Empty,
                dto.ImageUrl ?? string.Empty,
                (dto.Projects ?? new List<ProjectDto>()).Select(p => ProjectMapper.ToEntity(p)).ToList()
            );
            return experience;
        }

        public static ExperienceDto ToDto(Experience exp) => new ExperienceDto
        {
            Id = exp.Id,
            Company = exp.Company,
            Position = exp.Position,
            StartDate = exp.StartDate,
            EndDate = exp.EndDate,
            Description = exp.Description ?? string.Empty,
            Projects = exp.Projects.Select(p => ProjectMapper.ToDto(p)).ToList()
        };
    }
}