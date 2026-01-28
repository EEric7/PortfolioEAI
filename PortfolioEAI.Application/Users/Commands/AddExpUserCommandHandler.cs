using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddExpUserCommandHandler : IRequestHandler<AddExpUserCommand, Result<IEnumerable<Guid>>>
    {
        // Repositories
        private readonly IUserRepository _repository;
        private readonly IExperienceRepository _expRepository;

        /// <summary>
        ///  Constructor injecting the user repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="expRepository"></param>
        public AddExpUserCommandHandler(IUserRepository repository, IExperienceRepository expRepository)
        {
            _repository = repository;
            _expRepository = expRepository;
        }

        /// <summary>
        ///  Handles adding experiences to a user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<IEnumerable<Guid>>> Handle(AddExpUserCommand request, CancellationToken ct)
        {
            try
            {
                // Retrieve the user entity
                User? user = await _repository.Get(x => x.Id == request.UserId, ct);

                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");

                // Retrieve experiences to add
                IEnumerable<Guid> results = [];

                foreach (var expDto in request.Exp)
                {
                    var newExp = Experience.Create(expDto.Company!, expDto.Description!);
                    if (!user.AddExperience(newExp))
                        results.Append(newExp.Id);
                }

                // Save changes
                await _repository.Update(user, ct);

                // Return success with user Id
                string message = results.Any() ? "Experience(s) added successfully." : "All experiences already exist for the user.";
                return Result<IEnumerable<Guid>>.Success(results, message);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Guid>>.Failure($"Error adding experience to user: {ex.Message}");
            }
        }
    }
}