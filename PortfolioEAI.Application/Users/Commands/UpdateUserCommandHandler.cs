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
                // Validate the request
                if (request.UserDto.Id is null || request.UserDto.Id == Guid.Empty)
                    return Result<Guid>.Success(default,"Id user not provided.");

                // Retrieve the existing user entity
                User? entity = await _repository.Get(request.UserDto.Id.Value, ct);

                // If user not found, return success with default Guid
                if (entity is null)
                    return Result<Guid>.Success(default, "User not found.");
                
                // Update user properties
                entity.SetEmail(request.UserDto.Email);
                entity.SetPassword(request.UserDto.Password);
                entity!.SetLastname(request.UserDto?.LastName);
                entity.SetFirstname(request.UserDto?.FirstName);
                entity.SetDisplayName(request.UserDto?.UserName);
                entity.SetRoles(request.UserDto?.Roles);
                entity.SetAddress(request.UserDto?.Address);
                entity.SetDescription(request.UserDto?.Description);

                // TODO: Update skills and experiences
                //entity.SetSkills(request.UserDto!.Skills.Select(x => SkillMapper.ToEntity(x)));
                //entity.SetExperiences(request.UserDto!.Experiences.Select(x => ExperienceMapper.ToEntity(x)));

                // Save changes
                await _repository.Update(entity, ct);

                // Return the updated user DTO
                return Result<Guid>.Success(entity.Id,"User updated successfully.");
            }
            catch (Exception ex) 
            {
                return Result<Guid>.Failure($"Error updating user: {ex.Message}");
            }
        }
    }
}