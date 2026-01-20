using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    internal class ProjectMapper 
    {
        /// <summary>
        /// Maps a Project entity to a ProjectDto.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ProjectDto ToDto(Project p) 
        {
            return new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                //TODO: verify if ImageUrl should be Uri type
                Url = p.Url!.Value?? string.Empty
            };        
        }
    }
}