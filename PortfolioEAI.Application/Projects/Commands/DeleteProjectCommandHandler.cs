using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class DeleteProjectCommandHandler
    {
        //Dependencies
        private readonly IProjectRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public DeleteProjectCommandHandler(IProjectRepository repository) => _repository = repository;

        /// <summary>
        /// Handles the DeleteProjectCommand
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(DeleteProjectCommand request, CancellationToken ct)
        {
            try
            {
                //Get the existing project
                Project? project = await _repository.Get(x => x.Id == request.Id, ct);

                //If the project does not exist, return success with default Guid
                if (project == null)
                    return Result<Guid>.Success(default, "The project doesn't exist.");

                //Delete the project
                Guid result = project.Id;
                await _repository.Remove(project, ct);
                
                //Return the result
                return Result<Guid>.Success(result, "Project deleted successfully.");
            }
            catch (Exception ex)
            {   
                //Return failure result
                return Result<Guid>.Failure($"An error occurred while deleting the project: {ex.Message}");
            }
        }
    }
}