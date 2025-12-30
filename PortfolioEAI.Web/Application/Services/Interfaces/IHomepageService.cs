using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Application.Services.Interfaces
{
    public interface IHomepageService
    {
        Task<IEnumerable<AdminUserDto>> GetAdminUser();
    }
}