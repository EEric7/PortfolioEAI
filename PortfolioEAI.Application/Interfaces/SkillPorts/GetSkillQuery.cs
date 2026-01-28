using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public record GetAllSkillsQuery(string Criteria) : IRequest<Result<IEnumerable<SkillDto>>>;

    public record GetSkillQuery(Guid Id) : IRequest<Result<SkillDto?>>;
}