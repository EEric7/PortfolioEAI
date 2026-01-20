using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record CreateSkillCommand(string Name, string Level, string Category) : IRequest<Result<Guid>>;
}