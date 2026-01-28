
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
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;

        /// <summary>
        ///   Constructor injecting the user repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="skillRepository"></param>
        public AddSkillUserCommandHandler(IUserRepository userRepository, ISkillRepository skillRepository) 
        {
            _userRepository = userRepository;
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
                User? user = await _userRepository.Get(x => x.Id == request.UserId, ct);
                
                if (user is null)
                    return Result<IEnumerable<Guid>>.Success([], "User not found.");
                
                // Retrieve skills to add
                IEnumerable<Guid> results = [];
                foreach (Guid skillId in request.SkillIDs)
                {
                    Skill? skill = await _skillRepository.Get(x => x.Id == skillId, ct);

                    if (skill is not null)
                    {
                        user.AddSkill(skill);
                        results.Append(skill.Id);
                    }
                }

                // Save changes
                await _userRepository.Update(user, ct);

                // Return success with user Id
                string message = results.Any() ? $"Skills added successfully in User: {user.Id}." : "No new skills were added to User: {user.Id}.";
                return Result<IEnumerable<Guid>>.Success(results,message);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return Result<IEnumerable<Guid>>.Failure($"Error adding skill to user: {ex.Message}");
            }
        }
    }
}