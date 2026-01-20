using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteProjectCommand(Guid Id) : IRequest<Result<Guid>>;

    public sealed record DeleteProjectByTitleCommand(string Title) : IRequest<Result<Guid>>;
}