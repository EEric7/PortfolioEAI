using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Users.Queries;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<Guid>>>
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
    public async Task<Result<IEnumerable<Guid>>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        try
        {
            // Retrieve all users
            IEnumerable<User>? users = await _repository.GetAll(ct);

            if (users is null || !users.Any())
                return Result<IEnumerable<Guid>>.Success([],"No users found.");
            
            // Extract user IDs
            var userIds = users.Select(user => user.Id);

            // Return success result with user IDs
            return Result<IEnumerable<Guid>>.Success(userIds,"Users retrieved successfully.");
        }
        catch (Exception ex)
        {
            // Return failure result in case of an exception
            return Result<IEnumerable<Guid>>.Failure($"Error retrieving users: {ex.Message}");
        }
    }
}
