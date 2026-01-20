using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces.UserPorts
{
    public sealed record AddExpUserCommand(Guid UserId, params ExperienceDto[] Exp) : IRequest<Result<IEnumerable<Guid>>>;
}