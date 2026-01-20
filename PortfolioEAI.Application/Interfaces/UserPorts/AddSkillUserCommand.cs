using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces.UserPorts
{
    public sealed record AddSkillUserCommand(Guid UserId, params Guid[] SkillIDs) : IRequest<Result<IEnumerable<Guid>>>;
}