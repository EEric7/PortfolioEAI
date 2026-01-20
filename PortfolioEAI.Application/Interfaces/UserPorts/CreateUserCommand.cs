using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record CreateUserCommand(string Email, string Password, string FirstName, string LastName, string UserName, string Description) : IRequest<Result<Guid>>;
}