
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Services.Interfaces
{
    public interface IHomepageService
    {
        Task<IEnumerable<AdminUserDto>> GetAdminUser();
    }
}