using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Domain.Services.Interfaces;

namespace PortfolioEAI.Application.Services
{
    public class HomepageService : IHomepageService
    {
        private readonly ILogger<HomepageService> _logger;
        private readonly IServices _serviceDomain;

        public HomepageService(ILogger<HomepageService> logger, IServices serviceDomain)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "");
            _serviceDomain = serviceDomain ?? throw new ArgumentNullException(nameof(serviceDomain), "");
        }

        public async Task <IEnumerable<AdminUserDto>> GetAdminUser()
        {
            try
            {
                var adminUser = await _serviceDomain.AdminUserService.GetAllAsync();
                if (adminUser == null)
                {
                    _logger.LogWarning("AdminUser not found.");
                    return Enumerable.Empty<AdminUserDto>();
                }

                return adminUser.Select(user => Mappings.AdminUserMapper.ToDto(user));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving AdminUser: {ex.Message}");
                return Enumerable.Empty<AdminUserDto>();
            }
        }
    }
}