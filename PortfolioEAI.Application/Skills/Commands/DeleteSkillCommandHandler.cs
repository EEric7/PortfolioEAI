using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<Guid>>
    {
        // Repository for managing skills
        private readonly ISkillRepository _repository;

        /// <summary>
        ///   Constructor for DeleteSkillCommandHandler.
        /// </summary>
        /// <param name="repository"></param>
        public DeleteSkillCommandHandler(ISkillRepository repository) =>_repository = repository;

        /// <summary>
        ///   Handles the deletion of a skill by its ID.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>        
        public async Task<Result<Guid>> Handle(DeleteSkillCommand request, CancellationToken ct)
        {
            try
            {
                // Get the skill to delete
                Skill? skill = await _repository.Get(request.id, ct);
                
                // If the skill does not exist, return success with default GUID
                if (skill is null) 
                    return Result<Guid>.Success(default, "Skill not found.");

                // Remove the skill
                await _repository.Remove(skill, ct);

                // Retourne l'ID de la compétence supprimée
                return Result<Guid>.Success(request.id, "Skill deleted successfully.");
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                return Result<Guid>.Failure($"Erreur lors de la suppression de la compétence : {ex.Message}");
            }
        }
    }
}