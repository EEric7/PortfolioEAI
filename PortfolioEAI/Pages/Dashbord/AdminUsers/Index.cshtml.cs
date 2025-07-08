using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class IndexModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public IndexModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public IList<AdminUserDto> AdminUsers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                var AdminUsers = await _adminUserService.GetAllAsync();
                AdminUsers = AdminUsers.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
            }
        }
    }
}
