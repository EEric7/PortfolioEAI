using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands
{
    public class DeleteUserByEmailCommandHandler : IRequestHandler<DeleteUserByEmailCommand, Result<Guid>>
    {
        // Repository for user data access
        private readonly IUserRepository _repository;

        // Constructor injecting the user repository
        public DeleteUserByEmailCommandHandler(IUserRepository repository) => _repository = repository;

        // Handles the deletion of a user by their email
        public async Task<Result<Guid>> Handle(DeleteUserByEmailCommand request, CancellationToken ct)
        {
            try
            {
                // Retrieve the user by email
                User? entity = await _repository.Get(x => x.Email.Value == request.Email, ct);

                if (entity is null)
                    return Result<Guid>.Success(default, "User not found.");

                // Delete the user
                Guid result = entity.Id;
                await _repository.Remove(entity, ct);

                // Return the Id of the deleted user
                return Result<Guid>.Success(result, "User deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"Error deleting user: {ex.Message}");
            }
        }
    }
}