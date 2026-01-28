using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteExperienceCommand(Guid Id) : IRequest<Result<Guid>>;
}