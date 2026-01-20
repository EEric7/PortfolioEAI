using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Queries
{
    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, Result<SkillDto?>>
    {
        // Repository for accessing skill data
        private readonly ISkillRepository _repository;

        //a Constructor to inject the skill repository
        public GetSkillByIdHandler(ISkillRepository repository) => _repository = repository;

        // Handles the GetSkillByIdQuery request
        public async Task<Result<SkillDto?>> Handle(GetSkillByIdQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve the skill by ID from the repository
                Skill? skill = await _repository.Get(request.Id, ct);
                
                if (skill is null)
                    return Result<SkillDto?>.Success(default, "Skill not found.");

                // Map the Skill entity to SkillDto
                SkillDto? skillDto = SkillMapper.ToDto(skill);

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