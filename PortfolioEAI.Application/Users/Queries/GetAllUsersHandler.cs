using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;

namespace PortfolioEAI.Application.Users.Queries;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDto>>>
{
    // Repository for user data access
    private readonly IUserRepository _repository;

    /// <summary>
    ///   Constructor to inject the user repository.
    /// </summary>
    /// <param name="repository"></param>
    public GetAllUsersHandler(IUserRepository repository) => _repository = repository;

    /// <summary>
    ///   Handle method to process the GetAllUsersQuery.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        try
        {
            // Retrieve all users
            IEnumerable<User>? users = await _repository.GetAllBy(_ => true, ct);

            if (users is null || !users.Any())
                return Result<IEnumerable<UserDto>>.Success([],"No users found.");
            
            // Extract user IDs
            var userIds = users.Select(u => UserMapper.ToDto(u));

            // Return success result with user IDs
            return Result<IEnumerable<UserDto>>.Success(userIds,"Users retrieved successfully.");
        }
        catch (Exception ex)
        {
            // Return failure result in case of an exception
            return Result<IEnumerable<UserDto>>.Failure($"Error retrieving users: {ex.Message}");
        }
    }
}
