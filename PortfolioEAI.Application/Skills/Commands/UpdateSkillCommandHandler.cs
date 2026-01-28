using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class UpdateSkillCommandHandler
    {
        // Repository for managing skills
        private readonly ISkillRepository _repository;

        /// <summary>
        ///  Constructor for UpdateSkillCommandHandler.
        /// </summary>
        /// <param name="repository"></param>
        public UpdateSkillCommandHandler(ISkillRepository repository) => _repository = repository;

        /// <summary>
        ///  Handles the updating of an existing skill.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(UpdateSkillCommand request, CancellationToken ct)
        {
            try
            {
                // Get thr existing skill.
                Skill? skill = await _repository.Get(s => s.Id == request.SkillDto.Id, ct);

                if (skill is null)
                    return Result<Guid>.Success(default, "Skill not found.");

                //Update the skill properties.
                skill.SetName(request.SkillDto.Name);
                skill.SetCategory(request.SkillDto.Category);
                skill.SetLevel(request.SkillDto.Level);

                // Save the updated skill.
                await _repository.Update(skill, ct);
                
                // Return the updated skill ID.
                return Result<Guid>.Success(skill.Id, "Skill updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle errors.
                return Result<Guid>.Failure($"Erreur lors de la mise à jour de la compétence : {ex.Message}");
            }
        }
    }
}