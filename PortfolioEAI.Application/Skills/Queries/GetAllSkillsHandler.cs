using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Application.Skills.Queries
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, Result<IEnumerable<SkillDto>>>
    {
        // Repository for accessing skill data
        private readonly ISkillRepository _repository;
        
        // Constructor to inject the skill repository
        public GetAllSkillsHandler(ISkillRepository repository) => _repository = repository;

        // Handles the GetAllSkillsQuery request
        public async Task<Result<IEnumerable<SkillDto>>> Handle(GetAllSkillsQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve all skills from the repository
                IEnumerable<Skill> skills= [];

                if (!string.IsNullOrWhiteSpace(request.Criteria))
                    skills = await _repository.GetAllBy(s => s.Name.Contains(request.Criteria, StringComparison.OrdinalIgnoreCase), ct);
                else 
                    skills = await _repository.GetAllBy(_ => true, ct);

                // If no skills found, return empty list
                if (!skills.Any())
                    return Result<IEnumerable<SkillDto>>.Success([], "No skills found.");  
                
                // Extract skill 
                IEnumerable<SkillDto> skillDTOs = skills.Select(x => SkillMapper.ToDto(x)).ToList();
                
                // Return the list of skill IDs wrapped in a success result
                return Result<IEnumerable<SkillDto>>.Success(skillDTOs, "Skills retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Return a failure result with the exception message
                return Result<IEnumerable<SkillDto>>.Failure($"Error retrieving skills: {ex.Message}");
            }
        }
    }
}