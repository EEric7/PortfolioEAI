using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record UpdateProjectCommand(ProjectDto Project) : IRequest<Result<Guid>>;
}