using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public DeleteModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Get the admin user by ID
                var adminUser = await _adminUserService.GetByIdAsync(id);

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
                if (!await AdminUserExistsAsync(id))
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return RedirectToPage("./Index");
                }

                // Call the service to delete the admin user
                await _adminUserService.DeleteAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing admin users: {ex.Message}");
                return NotFound();
            }
        }

        private async Task<bool> AdminUserExistsAsync(Guid id)
        {
            return await _adminUserService.GetByIdAsync(id) != null;
        }
    }
}
