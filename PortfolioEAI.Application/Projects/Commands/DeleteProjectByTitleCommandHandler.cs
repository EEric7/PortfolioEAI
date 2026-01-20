

using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class DeleteProjectByTitleCommandHandler
    {
        //  Repository for project entity
        private readonly IProjectRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public DeleteProjectByTitleCommandHandler(IProjectRepository repository) => _repository = repository;

        /// <summary>
        /// Handles the DeleteProjectByTitleCommand
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(DeleteProjectByTitleCommand request, CancellationToken ct)
        {
            try
            {
                //Get the existing project by title
                var existingProject = await _repository.GetByTitle(request.Title, ct);

                //If the project does not exist, return success with default Guid
                if (existingProject == null)
                    return Result<Guid>.Success(default, "The project doesn't exist.");

                //Delete the project
                await _repository.Remove(existingProject, ct);
                
                //Return the result
                return Result<Guid>.Success(existingProject.Id, "Project deleted successfully.");
            }
            catch (Exception ex)
            {   
                //Return failure result
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}