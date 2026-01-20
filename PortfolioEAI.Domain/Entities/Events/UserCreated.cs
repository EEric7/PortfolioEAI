using PortfolioEAI.Domain.Common;

namespace PortfolioEAI.Domain.Entities.Events
{
    public sealed record class UserCreated(Guid UserId) : IDomainEvent
    {
        public DateTime OccurredAtUtc { get; } = DateTime.UtcNow;
    }
}