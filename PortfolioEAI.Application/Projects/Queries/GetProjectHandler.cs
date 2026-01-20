
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Queries
{
    public class GetProjectHandler
    {  
        //  Implements IRequestHandler to handle GetProjectQuery and return Result<ProjectDto?>
        private readonly IProjectRepository _repository;

        // Constructor to inject the project repository
        public GetProjectHandler(IProjectRepository repository) =>_repository = repository;

        // Handle method to process the GetProjectQuery
        public async Task<Result<ProjectDto?>> Handle(GetProjectQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve the project by ID from the repository
                var project = await _repository.Get(request.Id, ct);
                
                // If project not found, return success result with default value
                if (project == null)
                    return Result<ProjectDto?>.Success(default, "Project not found.");

                // Map the project entity to ProjectDto
                ProjectDto dto = Mappings.ProjectMapper.ToDto(project);

                return Result<ProjectDto?>.Success(dto, "Project retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<ProjectDto?>.Failure($"An error occurred while retrieving the project : {ex.Message}");
            }
        }
    }
}