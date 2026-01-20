using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record GetAllExperiencesQuery() : IRequest<Result<IEnumerable<Guid>>>;
    public sealed record GetExperienceQuery(Guid experienceId) : IRequest<Result<Guid>>;
    public sealed record GetExperiencesByCompanyQuery(string Company) : IRequest<Result<IEnumerable<Guid>>>;
    public sealed record GetExperiencesByProjectQuery(string ProjectTitle) : IRequest<Result<IEnumerable<Guid>>>;
}