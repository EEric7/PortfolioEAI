using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class DeleteExperienceByCompagnyCommandHandler
    {
        // Dependencies
        private readonly IExperienceRepository _repository;

        /// <summary>
        ///    Constructor
        /// </summary>
        /// <param name="repository"></param>
        public DeleteExperienceByCompagnyCommandHandler(IExperienceRepository repository) => _repository = repository;

        /// <summary>
        /// Handle Method
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> Handle(DeleteExperienceByCompanyCommand command, CancellationToken ct)
        {
            try
            {
                // Fetch the experiences by company
                Experience? experiences = await _repository.GetByCompany(command.Company, ct);

                //  Check if experiences exist
                if (experiences is null)
                    return Result<Guid>.Success(default, "No experiences found for the specified company.");

                // Delete the experiences
                await _repository.Remove(experiences, ct);

                // Return success result
                return Result<Guid>.Success(experiences.Id, "Experiences deleted successfully.");
            }
            catch (Exception ex)
            {
                // Error handling
                return Result<Guid>.Failure($"An error occurred while deleting experiences. : {ex.Message}");
            }
        }
    }
}