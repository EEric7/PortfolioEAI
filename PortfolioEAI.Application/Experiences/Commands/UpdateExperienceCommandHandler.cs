using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class UpdateExperienceCommandHandler
    {
        // Repository for experience data access
        private readonly IExperienceRepository _repository;

        // Constructor to inject the experience repository
        public UpdateExperienceCommandHandler(IExperienceRepository repository) => _repository = repository;

        // Handle method to process the update experience command
        public async Task<Result<Guid>> Handle(UpdateExperienceCommand command, CancellationToken ct)
        {
            try
            {
                //Fetch the existing experience
                Experience? experience = await _repository.Get(x => x.Id == command.Dto.Id, ct);

                // Check if experience exists
                if (experience is null)
                    return Result<Guid>.Success(default, "Experience not found.");

                // Update experience details
                experience.SetCompany(command.Dto.Company);
                experience.SetDescription(command.Dto.Description);
                // experience.SetImageUrl(command.Dto.ImageUrl);

                // Save the updated experience
                await _repository.Update(experience, ct);

                // Return success result
                return Result<Guid>.Success(experience.Id, "Experience updated successfully.");
            }
            catch (Exception ex)
            {
                // Error handling
                return Result<Guid>.Failure($"An error occurred while updating the experience: {ex.Message}");
            }
        }
    }
}