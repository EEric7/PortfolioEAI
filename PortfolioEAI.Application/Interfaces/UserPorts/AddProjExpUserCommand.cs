using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces.UserPorts
{
    public sealed record AddProjExpUserCommand(Guid UserId, Guid ExpID, params ProjectDto[] Projects) : IRequest<Result<IEnumerable<Guid>>>;
}