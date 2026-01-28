using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
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
                Project? project = await _repository.Get(x => x.Id == request.Project.Id, ct);

                if (project == null)
                    return Result<Guid>.Success(default, "The project doesn't exist.");

                // Mise à jour des propriétés du projet
                project.SetTitle(request.Project.Title);
                project.SetDescription(request.Project.Description);
                project.SetStartDate(request.Project.StartDate);
                project.SetEndDate(request.Project.EndDate);
                project.SetUrl(request.Project.Url);
                //TODO: Set ImageURL

                // Update the project in the database
                await _repository.Update(project, ct);

                //Return the result
                return Result<Guid>.Success(project.Id, "Project updated successfully.");
            }
            catch (Exception ex)
            {
                //Return failure result
                return Result<Guid>.Failure($"An error occurred while updating the project: {ex.Message}");
            }
        }
    }
}