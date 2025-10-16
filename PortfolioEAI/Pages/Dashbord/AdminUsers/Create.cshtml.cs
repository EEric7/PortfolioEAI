using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class CreateModel : PageModel
    {
        private readonly IDashbordService _services;

        public CreateModel(IDashbordService services)
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

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Validate the model state
                if (!ModelState.IsValid)
                    return Page();

                // Check if the admin user already exists
                if (await _services.AdminUserExistsAsync(AdminUser))
                {
                    ModelState.AddModelError(string.Empty, "An admin user with the same username and email already exists.");
                    return Page();
                }

                // Call the service to create the admin user
                await _services.CreateAdminUserAsync(AdminUser);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the admin user: {ex.Message}");
                return Page();
            }
        }
    }
}
