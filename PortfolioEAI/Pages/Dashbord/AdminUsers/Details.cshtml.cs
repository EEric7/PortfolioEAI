using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class DetailsModel : PageModel
    {
        private readonly IAdminUserService _servicesAdminUsers;
        
        public DetailsModel(IAdminUserService servicesAdminUsers)
        {
            _servicesAdminUsers = servicesAdminUsers;
        }

        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var adminUser = await _servicesAdminUsers.GetByIdAsync(id);

            if (adminUser is not null)
            {
                AdminUser = adminUser;

                return Page();
            }

            return NotFound();
        }
    }
}
