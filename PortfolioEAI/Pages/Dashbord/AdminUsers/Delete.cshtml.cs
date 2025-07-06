using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminUserService _servicesAdminUsers;

        public DeleteModel(IAdminUserService servicesAdminUsers)
        {
            _servicesAdminUsers = servicesAdminUsers;
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
           var adminUser = await _servicesAdminUsers.GetByIdAsync(id);

            if (adminUser is not null)
            {
                AdminUser = adminUser;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var adminUser = await _servicesAdminUsers.GetByIdAsync(id);

            if (adminUser != null)
            {
                AdminUser = adminUser;
                await _servicesAdminUsers.DeleteAsync(AdminUser.Id);
            }
            return RedirectToPage("./Index");
        }
    }
}
