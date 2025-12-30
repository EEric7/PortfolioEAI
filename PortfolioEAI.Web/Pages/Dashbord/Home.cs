using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.Services.Interfaces;
using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Pages.Dashbord
{
    public class HomeModel : PageModel
    {
        private readonly IDashbordService _services;

        public HomeModel(IDashbordService services)
        {
            _services = services;
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };
        
        public AdminUserDto? AdminUser { get; set; } = default;

        public async Task OnGetAsync()
        {
            try
            {
                //TODO Replace with actual user identification logic.
                AdminUser = (await _services.GetAllAdminUsersAsync()).ToList().FirstOrDefault();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
            }
        }
    }
}