using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.Services.Interfaces;
using PortfolioEAI.Web.Application.DTOs;

namespace PortfolioEAI.Web.Pages.Dashbord.AdminUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IDashbordService _services;

        public DeleteModel(IDashbordService services)
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

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Get the admin user by ID
                var adminUser = await _services.GetAdminUserByIdAsync(id);

                // Check if the admin user exists
                if (adminUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return Page();
                }

                // Assign the retrieved admin user to the property
                AdminUser = adminUser;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                 // Validate the model state
                if (!ModelState.IsValid)
                    return Page();

                // Check if the admin user exists
                if (!await _services.AdminUserExistsAsync(AdminUser))
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return RedirectToPage("./Index");
                }

                // Call the service to delete the admin user
                await _services.DeleteAdminUserByIdAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing admin users: {ex.Message}");
                return NotFound();
            }
        }
    }
}
