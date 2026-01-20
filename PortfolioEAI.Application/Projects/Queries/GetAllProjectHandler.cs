using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Queries
{
    public class GetAllProjectHandler
    {
        //  Implements IRequestHandler to handle GetAllProjectsQuery and return Result<IEnumerable<Guid>>
        private readonly IProjectRepository _repository;

        // Constructor to inject the project repository
        public GetAllProjectHandler(IProjectRepository repository) => _repository = repository;

        // Handle method to process the GetAllProjectsQuery
        public async Task<Result<IEnumerable<Guid>>> Handle(GetAllProjectsQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve all projects from the repository
                IEnumerable<Project> projects = await _repository.GetAll(ct);

                // If no projects found, return an empty list
                if (projects == null || !projects.Any())
                    return Result<IEnumerable<Guid>>.Success([], "No projects found.");

                // Map projects to their IDs
                IEnumerable<Guid> projectIds = projects.Select(p => p.Id);

                // Return the list of project IDs wrapped in a success result
                return Result<IEnumerable<Guid>>.Success(projectIds, "Projects retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a failure result
                return Result<IEnumerable<Guid>>.Failure($"An error occurred while retrieving projects: {ex.Message}");
            }
        }
    }
}