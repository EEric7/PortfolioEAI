using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Data;
using PortfolioEAI.Models;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly Services.IServices _services;

        public CreateModel(Services.IServices services)
        {
            _services = services;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _services.Projects.AddAsync(Project);

            return RedirectToPage("./Index");
        }
    }
}
