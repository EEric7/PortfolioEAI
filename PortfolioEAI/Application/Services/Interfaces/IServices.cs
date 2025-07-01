using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Services
{
    public interface IServices
    {
        IGenericServices<ProjectDto> Projects { get; }
    }
}