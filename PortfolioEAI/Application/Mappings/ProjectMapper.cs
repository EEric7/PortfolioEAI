using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    public class ProjectMapper 
    {
        /// <summary>
        /// Maps a Project entity to a ProjectDto.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ProjectDto ToDto(Project p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Url = p.Url.ToString()
        };

        /// <summary>
        /// Maps a ProjectDto to a Project entity.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Project ToEntity(ProjectDto dto) => new(
            dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            dto.Title ?? string.Empty,
            dto.Description ?? string.Empty,
            dto.ImageUrl ?? string.Empty,
            dto.Url ?? string.Empty) { };
    }
}