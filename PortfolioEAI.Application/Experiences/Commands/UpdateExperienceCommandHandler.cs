using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class UpdateExperienceCommandHandler
    {
        private readonly IExperienceRepository _repository;

        public UpdateExperienceCommandHandler(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(UpdateExperienceCommand command, CancellationToken ct)
        {
            try
            {
                //Fetch the existing experience
                Experience? experience = await _repository.Get(command.experience.Id, ct);

                // Check if experience exists
                if (experience is null)
                    return Result<Guid>.Success(default, "Experience not found.");

                // Update experience details
                experience.SetCompany(command.experience.Company);
                experience.SetPosition(command.experience.Position);
                experience.SetDescription(command.experience.Description);

                //TODO: Update ImageUrl

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