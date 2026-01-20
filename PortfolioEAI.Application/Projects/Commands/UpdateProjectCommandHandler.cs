using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class UpdateProjectCommandHandler
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectCommandHandler(IProjectRepository repository) => _repository = repository;

        public async Task<Result<Guid>> Handle(UpdateProjectCommand request, CancellationToken ct)
        {
            try
            {
                // Récupérer le projet existant
                var existingProject = await _repository.Get(request.Project.Id, ct);

                if (existingProject == null)
                    return Result<Guid>.Success(default, "The project doesn't exist.");

                // Mise à jour des propriétés du projet
                existingProject.SetTitle(request.Project.Title);
                existingProject.SetDescription(request.Project.Description);
                existingProject.SetStartDate(request.Project.StartDate);
                existingProject.SetEndDate(request.Project.EndDate);
                existingProject.SetUrl(request.Project.Url);
                //TODO: Set ImageURL

                // Update the project in the database
                await _repository.Update(existingProject, ct);

                //Return the result
                return Result<Guid>.Success(existingProject.Id, "Project updated successfully.");
            }
            catch (Exception ex)
            {
                //Return failure result
                return Result<Guid>.Failure($"An error occurred while updating the project: {ex.Message}");
            }
        }
    }
}