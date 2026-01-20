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
                User? user = await _repository.Get(request.UserId, ct);

                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");

                bool exists = user.Experiences.Any(e => e.Id == request.ExpID);

                if (!exists)
                    return Result<IEnumerable<Guid>>.Success([], "Experience not found in user.");

                // Retrieve projects to add
                IEnumerable<Project> projects = request.Projects.Select(x => Project.Create(x.Title, x.Description, x.StartDate, x.EndDate));
                user.AddProject(request.ExpID, projects);

                // Save changes
                await _repository.Update(user, ct);

                // Prepare result
                var result = projects.Select(p => p.Id);

                // Return success with project Ids
                string message = result.Any() ? "" : "However, no projects were added.";
                return Result<IEnumerable<Guid>>.Success(result, "Project(s) added successfully." + message);
            }
            catch (Exception ex)
            {
                // Return failure in case of an exception
                return Result<IEnumerable<Guid>>.Failure($"An error occurred while adding projects to experience: {ex.Message}");
            }
        }
    }
}