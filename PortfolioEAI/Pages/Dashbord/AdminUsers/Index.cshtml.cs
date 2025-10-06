using Microsoft.AspNetCore.Mvc;
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
        
        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "#"),
            new Tuple<string, string>("Skills", "#"),
            new Tuple<string, string>("Experiences", "#"),
            new Tuple<string, string>("Setting", "#"),
            new Tuple<string, string>("SignOut", "#")
        };

        public IList<AdminUserDto> AdminUsers { get; set; } = new List<AdminUserDto>();

        public async Task OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                AdminUsers = (await _adminUserService.GetAllAsync()).ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
            }
        }
    }
}
