using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data;
using PortfolioEAI.Models;

namespace PortfolioEAI.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly Services.IServices _services;

        public DeleteModel(Services.IServices services)
        {
            _services = services;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _services.Projects.GetByIdAsync((int)id);

            if (project is not null)
            {
                Project = project;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _services.Projects.GetByIdAsync((int)id);

            if (project != null)
            {
                Project = project;
                await _services.Projects.DeleteAsync(Project.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
