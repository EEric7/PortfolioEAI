
using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Application.Users.Queries
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, Result<UserDto?>>
    {
        // Repository for user data access
        private readonly IUserRepository _repository;

        /// <summary>
        /// Constructor injecting the user repository
        /// </summary>
        /// <param name="repository"></param>
        public GetUserByEmailHandler(IUserRepository repository) => _repository = repository;

        /// <summary>
        /// Handle method to process the GetUserByEmailQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result<UserDto?>> Handle(GetUserByEmailQuery request, CancellationToken ct)
        {   
            try
            {
                // Retrieve user by email
                User? user = await _repository.Get(x => x.Email.Value == request.Email, ct);

                if (user is null)
                    return Result<UserDto?>.Success(default,"User not found.");

                // Map to DTO
                UserDto? userDto = UserMapper.ToDto(user);

                // Return success result with user DTO
                return Result<UserDto?>.Success(userDto,"User retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Return failure result in case of an exception
                return Result<UserDto?>.Failure($"Error retrieving user: {ex.Message}");
            }
        }
    }
}