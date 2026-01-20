using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteSkillCommand(Guid id) : IRequest<Result<Guid>>;
    public sealed record DeleteSkillsByName(string name) : IRequest<Result<IEnumerable<Guid>>>;
}