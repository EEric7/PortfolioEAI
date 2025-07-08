using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class EditModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public EditModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Fetch the admin user by ID
                var adminUser = await _adminUserService.GetByIdAsync(id);

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

    
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Validate the model state
                if (!ModelState.IsValid)
                    return Page();

                // Check if the admin user exists
                if (!await AdminUserExistsAsync(AdminUser.Id))
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return Page();
                }

                // Call the service to update the admin user
                await _adminUserService.UpdateAsync(AdminUser);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the admin user: {ex.Message}");
                return Page();
            }  
        }

        private async Task<bool> AdminUserExistsAsync(Guid id)
        {
            return await _adminUserService.GetByIdAsync(id) != null;
        }
    }
}
