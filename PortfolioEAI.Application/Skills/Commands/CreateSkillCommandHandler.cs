using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Result<Guid>>
    {
        // Repository for managing skills
        private readonly ISkillRepository _repository;

        /// <summary>
        /// Constructor for CreateSkillCommandHandler.
        /// </summary>
        /// <param name="repository"></param>
        public CreateSkillCommandHandler(ISkillRepository repository) => _repository = repository;

        /// <summary>
        /// Handles the creation of a new skill.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken ct)
        {
            try
            {
                // Vérification de l'existence d'une compétence avec le même nom
                bool existing = await _repository.Exists(x => x.Name == request.Dto.Name, ct);

                if(existing)
                    return Result<Guid>.Success(default, "A skill with the same name already exists.");

                // Création de la compétence
                var newSkill = Skill.Create(request.Dto.Name, request.Dto.Level, request.Dto.Category);
                
                // Ajout de la compétence à la base de données
                await _repository.Add(newSkill, ct);

                // Retourne l'ID de la nouvelle compétence
                return Result<Guid>.Success(newSkill.Id, $"Skill created successfully.");
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                return Result<Guid>.Failure($"Erreur lors de la création de la compétence : {ex.Message}");
            }
        }
    }
}