using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
    {
        // Repository for user data access
        private readonly IUserRepository _repository;

        // Constructor injecting the user repository
        public UpdateUserCommandHandler(IUserRepository repository) => _repository = repository;

        // Handles the update of a user
        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            try
            {
                // Retrieve the existing user entity
                User? entity = await _repository.Get(x => x.Id == request.UserDto.Id, ct);

                // If user not found, return success with default Guid
                if (entity is null)
                    return Result<Guid>.Success(default, "User not found.");
                
                // Update user properties
                entity.SetFirstname(request.UserDto?.FirstName);
                entity!.SetLastname(request.UserDto?.LastName);
                entity.SetDescription(request.UserDto?.Description);
                entity.SetAddress(request.UserDto?.Address);

                // Save changes
                Guid result = entity.Id;
                await _repository.Update(entity, ct);

                // Return the updated user DTO
                return Result<Guid>.Success(result,"User updated successfully.");
            }
            catch (Exception ex) 
            {
                return Result<Guid>.Failure($"Error updating user: {ex.Message}");
            }
        }
    }
}