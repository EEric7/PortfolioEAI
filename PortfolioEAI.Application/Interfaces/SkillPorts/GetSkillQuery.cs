
using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public record GetAllSkillsQuery() : IRequest<Result<IEnumerable<Guid>>>;

    public record GetSkillByIdQuery(Guid Id) : IRequest<Result<SkillDto?>>;

    public record GetSkillByNameQuery(string Name) : IRequest<Result<SkillDto?>>;

    public record GetSkillsByCategoryQuery(string Category) : IRequest<Result<IEnumerable<Guid>>>;
}