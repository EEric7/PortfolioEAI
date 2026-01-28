using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class DeleteSkillByNameCommandHandler
    {
        // Unit of work for managing transactions
        private readonly ISkillRepository _repository;

        /// <summary>
        /// Constructor for DeleteSkillByNameCommandHandler.
        /// </summary>
        /// <param name="repository"></param>
        public DeleteSkillByNameCommandHandler(ISkillRepository repository) =>_repository = repository;

        /// <summary>
        /// Handles the deletion of skills by name.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(DeleteSkillsByNameCommand request, CancellationToken ct)
        {
            try
            {
                // Get skills to delete by name
                Skill? skill = await _repository.Get(x => x.Name == request.Name, ct);

                if (skill is null)
                    return Result<Guid>.Success(default, "No skills found with the provided name.");

                Guid deletedSkillId = skill.Id;

                // Delete the skills
                await _repository.Remove(skill, ct);

                // Return the ID of the deleted skill
                return Result<Guid>.Success(deletedSkillId, "Skills deleted successfully.");
            }
            catch (Exception ex)
            {
                // Error handling
                return Result<Guid>.Failure($"Error deleting skill: {ex.Message}");
            }
        }
    }
}