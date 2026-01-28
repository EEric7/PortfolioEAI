using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddProjectExpUserCommandHandler : IRequestHandler<AddProjExpUserCommand, Result<IEnumerable<Guid>>>
    {
        // Repository for user data access
        private readonly IUserRepository _repository;

        /// <summary>
        ///   Constructor injecting the user repository
        /// </summary>
        /// <param name="repository"></param>
        public AddProjectExpUserCommandHandler(IUserRepository repository) => _repository = repository;

        /// <summary>
        /// Handles adding projects to a user's experience
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<IEnumerable<Guid>>> Handle(AddProjExpUserCommand request, CancellationToken ct)
        {
            try
            {
                // Retrieve the user entity
                User? user = await _repository.Get(x => x.Id == request.UserId, ct);

                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");

                // Retrieve projects to add
                IEnumerable<Guid> result = [];
                foreach (var projectDto in request.Projects)
                {   
                    Project project = Project.Create(projectDto.Title, projectDto.Description, projectDto.Position, projectDto.StartDate, projectDto.EndDate);
                    if (user.AddProject(request.ExpID, project))
                        result.Append(project.Id);
                }  

                // Save changes
                await _repository.Update(user, ct);

                // Return success with project Ids
                string message = result.Any() ? "Project(s) added successfully." : "However, no projects were added.";
                return Result<IEnumerable<Guid>>.Success(result, message);
            }
            catch (Exception ex)
            {
                // Return failure in case of an exception
                return Result<IEnumerable<Guid>>.Failure($"An error occurred while adding projects to experience: {ex.Message}");
            }
        }
    }
}