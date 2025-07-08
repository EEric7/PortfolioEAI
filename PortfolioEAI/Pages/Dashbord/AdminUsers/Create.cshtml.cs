using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class CreateModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public CreateModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Validate the model state
                if (!ModelState.IsValid)
                    return Page();

                // Check if the admin user already exists
                if (await AdminUserExistsAsync(AdminUser))
                {
                    ModelState.AddModelError(string.Empty, "An admin user with the same username and email already exists.");
                    return Page();
                }

                // Call the service to create the admin user
                await _adminUserService.AddAsync(AdminUser);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the admin user: {ex.Message}");
                return Page();
            }
        }

        private async Task<bool> AdminUserExistsAsync(AdminUserDto dto)
        {
            var adminUsers = await _adminUserService.GetAllAsync();
            var existingAdminUser = adminUsers.FirstOrDefault(e => e.UserName == dto.UserName && e.Email == dto.Email);
            return existingAdminUser is not null;
        }
    }
}
