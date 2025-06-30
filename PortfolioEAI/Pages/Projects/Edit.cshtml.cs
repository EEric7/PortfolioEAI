using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Data;
using PortfolioEAI.Models;

namespace PortfolioEAI.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly Services.IServices _services;

        public EditModel(Services.IServices services)
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

            var project =  await _services.Projects.GetByIdAsync((int) id);
            if (project == null)
            {
                return NotFound();
            }
            Project = project;
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

            if(!ProjectExists(Project.Id))
            {
                return NotFound();
            }

            try
                {
                    await _services.Projects.UpdateAsync(Project);
                }
                catch (ArgumentNullException)
                {
                    throw;
                }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return _services.Projects.GetByIdAsync(Project.Id).Result != null;
        }
    }
}
