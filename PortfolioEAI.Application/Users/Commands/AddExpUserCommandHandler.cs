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
                User? user = await _repository.Get(request.UserId, ct);

                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");

                // Retrieve experiences to add
                IEnumerable<Experience> expsToAdd = [];

                foreach (var expDto in request.Exp)
                {
                    bool exists = await _expRepository.ExistsByCompany(expDto.Company!, ct);

                    if (exists)
                        continue;

                    Experience exp = Experience.Create(expDto.Company!, expDto.Position!, expDto.Description!);

                    if (exp is not null)
                        expsToAdd = expsToAdd.Append(exp);
                }

                // Check for experiences that were not found
                var expsNotFound = request.Exp.Select(e => e.Company).Except(expsToAdd.Select(e => e.Company));

                // Add experiences to user
                user.AddExperience(expsToAdd);

                // Save changes
                await _repository.Update(user, ct);

                // Return the result
                var result = user.Experiences.Select(e => e.Id);

                // Return success with user Id
                string message = expsNotFound.Any() ? $"However, the following experiences were not added as they already exist: {string.Join(", ", expsNotFound)}" : "";
                return Result<IEnumerable<Guid>>.Success(result, "Experience(s) added successfully." + message);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Guid>>.Failure($"Error adding experience to user: {ex.Message}");
            }
        }
    }
}