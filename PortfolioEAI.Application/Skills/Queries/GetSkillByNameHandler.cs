using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Queries
{
    public class GetSkillByNameHandler : IRequestHandler<GetSkillByNameQuery, Result<SkillDto?>>
    {
        private readonly ISkillRepository _repository;

        public GetSkillByNameHandler(ISkillRepository repository) => _repository = repository;

        public async Task<Result<SkillDto?>> Handle(GetSkillByNameQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve the skill by name from the repository
                Skill? skill = await _repository.GetByName(request.Name, ct);

                // If skill not found, return success with default value
                if (skill is null)
                    return Result<SkillDto?>.Success(default, "Skill not found.");

                // Map the Skill entity to SkillDto
                SkillDto skillDto = SkillMapper.ToDto(skill);

                // Return the SkillDto wrapped in a success result
                return Result<SkillDto?>.Success(skillDto, "Skill found.");
            }
            catch (Exception ex)
            {
                // Return a failure result with the exception message
                return Result<SkillDto?>.Failure($"Error retrieving skill: {ex.Message}");
            }
        }
    }
}