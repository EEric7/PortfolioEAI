using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Queries
{
    public class GetExperienceByIdHandler
    {
        // Repository for accessing experience data
        private readonly IExperienceRepository _repository;

        // Constructor to initialize the repository
        public GetExperienceByIdHandler(IExperienceRepository repository) => _repository = repository;

        // Method to handle the retrieval of an experience by its ID
        public async Task<Result<ExperienceDto?>> Handle(GetExperienceQuery request, CancellationToken ct)
        {
            try
            {
                // Fetch the experience by ID
                Experience? experience = await _repository.Get(x => x.Id == request.Id, ct);

                // Check if experience exists
                if (experience is null)
                    return Result<ExperienceDto?>.Success(default, "Experience not found.");

                // Map the experience entity to DTO
                ExperienceDto result = ExperienceMapper.ToDto(experience);

                // Return success result with the mapped DTO
                return Result<ExperienceDto?>.Success(result, "Experience retrieved successfully.");
            }
            catch (Exception)
            {
                return Result<ExperienceDto?>.Failure("An error occurred while retrieving the experience.");
            }
        }
    }
}