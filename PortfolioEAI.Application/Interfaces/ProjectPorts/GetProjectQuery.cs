using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record GetProjectQuery(Guid Id) : IRequest<Result<ProjectDto?>>;

    public sealed record GetProjectByTitleQuery(string Title) : IRequest<Result<ProjectDto?>>;

    public sealed record GetAllProjectsQuery() : IRequest<Result<IEnumerable<Guid>>>;
}