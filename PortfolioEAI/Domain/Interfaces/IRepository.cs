
using PortfolioEAI.Data.Repositories;

namespace PortfolioEAI.Domain.Interfaces
{
    public interface IRepository
    {
        ProjectRepository Projects { get; }
    }
}