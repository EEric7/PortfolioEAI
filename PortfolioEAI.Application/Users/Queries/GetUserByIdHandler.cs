using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Users.Queries;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto?>>
{
    // Repository for user data access
    private readonly IUserRepository _repository;

    /// <summary>
    /// Constructor injecting the user repository
    /// </summary>
    /// <param name="repository"></param>
    public GetUserByIdHandler(IUserRepository repository) => _repository = repository;
    
    /// <summary>
    /// Handle method to process the GetUserByIdQuery
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<Result<UserDto?>> Handle(GetUserByIdQuery request, CancellationToken ct)
    {   
        try
        {
            // Get the user by ID
            User? user = await _repository.Get(x => x.Id == request.Id, ct);

            // Check if user exists
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
