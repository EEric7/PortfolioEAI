using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord
{
    public class IndexModel : PageModel
    {
        private readonly IService _services;

        public IndexModel(IService _services)
        {
            this._services = _services;
        }
        
        public AdminUserDto? AdminUser { get; set; } = default;

        public async Task OnGetAsync()
        {
            try
            {
                AdminUser = (await _services.AdminUserService.GetAllAsync()).ToList().FirstOrDefault();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
            }
        }
    }
}