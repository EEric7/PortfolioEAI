using MediatR;
using PortfolioEAI.Application.Common;

namespace PortfolioEAI.Application.Interfaces
{
    public sealed record DeleteUserByEmailCommand(string Email): IRequest<Result<Guid>>;

    public sealed record DeleteUserByIdCommand(Guid Id): IRequest<Result<Guid>>;
}