using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class DetailsModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public DetailsModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                //Fetch the admin user by ID
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
    }
}
