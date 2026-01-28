using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteSkillCommand(Guid Id) : IRequest<Result<Guid>>;
    public sealed record DeleteSkillsByNameCommand(string Name) : IRequest<Result<IEnumerable<Guid>>>;
}