using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteExperienceCommand(Guid Id) : IRequest<Result<Guid>>;

    public sealed record DeleteExperienceByCompanyCommand(string Company) : IRequest<Result<Guid>>;
}