using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Pages.Dashbord.AdminUsers
{
    public class CreateModel : PageModel
    {
        private readonly IAdminUserService _servicesAdminUsers;

        public CreateModel(IAdminUserService servicesAdminUsers)
        {
            _servicesAdminUsers = servicesAdminUsers;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AdminUserDto AdminUser { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _servicesAdminUsers.AddAsync(AdminUser);
            return RedirectToPage("./Index");
        }
    }
}
