using MediatR;
using PortfolioEAI.Application.Common;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record UpdateExperienceCommand(ExperienceDto Dto) : IRequest<Result<Guid>>;
}