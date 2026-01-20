
using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddSkillUserCommandHandler : IRequestHandler<AddSkillUserCommand, Result<IEnumerable<Guid>>>
    {
        // Repository for user data access
        private readonly IUserRepository _repository;
        private readonly ISkillRepository _skillRepository;

        /// <summary>
        ///   Constructor injecting the user repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="skillRepository"></param>
        public AddSkillUserCommandHandler(IUserRepository repository, ISkillRepository skillRepository)
        {
            _repository = repository;
            _skillRepository = skillRepository;
        }

        /// <summary>
        /// Handles adding skills to a user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<IEnumerable<Guid>>> Handle(AddSkillUserCommand request, CancellationToken ct)
        {
            try
            {
                // Retrieve the user entity
                User? user = await _repository.Get(request.UserId, ct);
                
                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");
                
                // Retrieve skills to add
                IEnumerable<Skill> skillsToAdd = [];

                foreach (Guid skillId in request.SkillIDs)
                {
                    Skill? skill = await _skillRepository.Get(skillId, ct);

                    if (skill is not null)
                        skillsToAdd = skillsToAdd.Append(skill);
                }

                // Check for skills that were not found
                IEnumerable<Guid> skillsNotFound = request.SkillIDs.Except(skillsToAdd.Select(s => s.Id));

                // Add skills to user
                user.AddSkill(skillsToAdd);

                // Save changes
                await _repository.Update(user, ct);

                // Return success with user Id
                string message = skillsNotFound.Any() ? $"However, the following skills were not added as they were not found: {string.Join(", ", skillsNotFound)}" : "";
                return Result<IEnumerable<Guid>>.Success(skillsToAdd.Select(s => s.Id), $"Skills added successfully in User: {user.Id}." + message);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return Result<IEnumerable<Guid>>.Failure($"Error adding skill to user: {ex.Message}");
            }
        }
    }
}