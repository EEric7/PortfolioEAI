using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record CreateSkillCommand(SkillDto Dto) : IRequest<Result<Guid>>;
}