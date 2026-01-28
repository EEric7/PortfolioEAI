using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces;

public record GetAllUsersQuery() : IRequest<Result<IEnumerable<UserDto>>>;

public record GetUserByEmailQuery(string Email) : IRequest<Result<UserDto?>>;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto?>>;

