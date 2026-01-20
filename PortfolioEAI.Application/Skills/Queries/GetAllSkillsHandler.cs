using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Skills.Queries
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, Result<IEnumerable<Guid>>>
    {
        // Repository for accessing skill data
        private readonly ISkillRepository _repository;
        
        // Constructor to inject the skill repository
        public GetAllSkillsHandler(ISkillRepository repository) => _repository = repository;

        // Handles the GetAllSkillsQuery request
        public async Task<Result<IEnumerable<Guid>>> Handle(GetAllSkillsQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve all skills from the repository
                var skills = await _repository.GetAll(ct);

                // If no skills found, return empty list
                if (skills == null || !skills.Any())
                    return Result<IEnumerable<Guid>>.Success([], "No skills found.");  
                
                // Extract skill IDs
                IEnumerable<Guid> skillIds = skills.Select(s => s.Id);
                
                // Return the list of skill IDs wrapped in a success result
                return Result<IEnumerable<Guid>>.Success(skillIds, "Skills retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Return a failure result with the exception message
                return Result<IEnumerable<Guid>>.Failure($"Error retrieving skills: {ex.Message}");
            }
        }
    }
}