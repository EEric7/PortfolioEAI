using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Queries
{
    public class GetProjectByTitleHandler
    {
        //  Implements IRequestHandler to handle GetProjectByTitleQuery and return Result<ProjectDto?>
        private readonly IProjectRepository _repository;

        //  Constructor to inject the project repository    
        public GetProjectByTitleHandler(IProjectRepository repository) => _repository = repository;

        // Handle method to process the GetProjectByTitleQuery
        public async Task<Result<ProjectDto?>> Handle(GetProjectByTitleQuery request, CancellationToken ct)
        {
           try
            {
                // Retrieve the project by title from the repository
                var project = await _repository.GetByTitle(request.Title, ct);
                
                // If project not found, return success result with default value
                if (project == null)
                    return Result<ProjectDto?>.Success(default, "Project not found.");

                // Map the project entity to ProjectDto
                ProjectDto dto = ProjectMapper.ToDto(project);

                return Result<ProjectDto?>.Success(dto, "Project retrieved successfully.");
            }
            catch (Exception)
            {
                return Result<ProjectDto?>.Failure("An error occurred while retrieving the project");
            }
        }
    }
}