using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, Result<Guid>>
    {
        // Dependencies
        private readonly IExperienceRepository _repository;

        // Constructor
        public DeleteExperienceCommandHandler(IExperienceRepository repository) => _repository = repository;

        // Handle Method
        public async Task<Result<Guid>>Handle(DeleteExperienceCommand command, CancellationToken ct)
        {
            try
            {
                // Fetch the experience to be deleted
                Experience? experience = await _repository.Get(command.Id, ct);

                if (experience is null)
                    return Result<Guid>.Success(default, "Experience not found.");
                
                // Delete the experience
                await _repository.Remove(experience, ct);

                // Return success result
                return Result<Guid>.Success(experience.Id, "Experience deleted successfully.");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while deleting the experience. : {ex.Message}");
            }
        }
    }
}