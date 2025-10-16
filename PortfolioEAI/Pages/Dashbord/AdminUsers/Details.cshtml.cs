using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class DetailsModel : PageModel
    {
        private readonly IDashbordService _services;

        public DetailsModel(IDashbordService services)
        {
            _services = services;
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Projects", "/Dashbord/Projects/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                //Fetch the admin user by ID
                var adminUser = await _services.GetAdminUserByIdAsync(id);

                if (adminUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return RedirectToPage("./Index");
                }

                AdminUser = adminUser;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                return NotFound();
            }
        }
    }
}
