using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record GetAllExperiencesQuery() : IRequest<Result<IEnumerable<ExperienceDto>>>;
    public sealed record GetExperienceQuery(Guid Id) : IRequest<Result<ExperienceDto?>>;
    public sealed record GetExperiencesByCompanyQuery(string Company) : IRequest<Result<ExperienceDto?>>;
}