using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Queries
{
    public class GetAllExperienceHandler
    {
        //  Repository for accessing experience data
        private readonly IExperienceRepository _repository;

        // Constructor to initialize the repository
        public GetAllExperienceHandler(IExperienceRepository repository) => _repository = repository;

        // Method to handle the retrieval of all experience IDs
        public async Task<Result<IEnumerable<ExperienceDto>>> Handle(GetAllExperiencesQuery request, CancellationToken ct)
        {
            try
            {
                // Retrieve all experiences
                IEnumerable<Experience>? experiences = await _repository.GetAllBy(_ => true, ct);

                // Check if any experiences were found
                if (experiences is null || !experiences.Any())
                    return Result<IEnumerable<ExperienceDto>>.Success([], "No experiences found.");

                // Extract IDs from experiences
                IEnumerable<ExperienceDto> resultats = experiences.Select(e => ExperienceMapper.ToDto(e));

                // Return success result with IDs
                return Result<IEnumerable<ExperienceDto>>.Success(resultats.ToList(), "Experiences retrieved successfully.");
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                return Result<IEnumerable<ExperienceDto>>.Failure("An error occurred while retrieving experiences.");
            }
        }
    }
}