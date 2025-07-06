using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class EditModel : PageModel
    {
        private readonly IAdminUserService _servicesAdminUsers;

        public EditModel(IAdminUserService servicesAdminUsersand)
        {
            _servicesAdminUsers = servicesAdminUsersand;
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            if(!AdminUserExists(AdminUser.Id))
                return NotFound();

            try
            {
                await _servicesAdminUsers.UpdateAsync(AdminUser);
            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!AdminUserExists(AdminUser.Id))
                return NotFound();

            try
            {
                await _servicesAdminUsers.UpdateAsync(AdminUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminUserExists(AdminUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdminUserExists(Guid id)
        {
            return _servicesAdminUsers.GetByIdAsync(AdminUser.Id).Result != null;
        }
    }
}
