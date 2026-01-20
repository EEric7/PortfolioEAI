
using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Queries
{
    public class GetSkillsByCategoryHandler : IRequestHandler<GetSkillsByCategoryQuery, Result<IEnumerable<Guid>>>
    {
        // Repository for accessing skill data
        private readonly ISkillRepository _repository;

        // Constructor to inject the skill repository
        public GetSkillsByCategoryHandler(ISkillRepository repository) => _repository = repository;

        // Handles the GetSkillsByCategoryQuery request
        public async Task<Result<IEnumerable<Guid>>> Handle(GetSkillsByCategoryQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve skills by category from the repository
                IEnumerable<Skill>? skills = await _repository.GetAllByCategory(request.Category, ct);

                // If no skills found, return empty list
                if (skills is null)
                    return Result<IEnumerable<Guid>>.Success([], "No skills found.");

                // Extract skill IDs 
                IEnumerable<Guid> skillIds = skills.Select(skill => skill.Id);
                
                // Return the list of skill IDs wrapped in a success result
                return Result<IEnumerable<Guid>>.Success(skillIds, "Skills retrieved successfully.");
            }
            catch (Exception ex)
            {
                // In case of an error, return a failure result with the error message
                return Result<IEnumerable<Guid>>.Failure($"Error retrieving skills: {ex.Message}");
            }
        }
    }
}