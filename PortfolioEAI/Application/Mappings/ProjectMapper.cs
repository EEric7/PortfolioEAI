using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    public class ProjectMapper
    {
        public static ProjectDto ToDto(Project p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Url = p.Url.ToString()
        };

        public static Project ToEntity(ProjectDto dto) => new(dto.Id, dto.Title ?? string.Empty, dto.Description ?? string.Empty, dto.ImageUrl ?? string.Empty, dto.Url ?? string.Empty) { };
    }
}