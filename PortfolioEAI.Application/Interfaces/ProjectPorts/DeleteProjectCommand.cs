using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteProjectCommand(Guid Id) : IRequest<Result<Guid>>;
}