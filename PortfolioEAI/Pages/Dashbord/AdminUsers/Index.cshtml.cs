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
            var projects = await _adminUserService.GetAllAsync();
            AdminUsers = projects.ToList();
        }
    }
}
