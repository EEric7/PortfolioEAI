using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;

namespace PortfolioEAI.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IServices _services;

        public EditModel(IServices services)
        {
            _services = services;
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var project =  await _services.Projects.GetByIdAsync(id);
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
                return Page();

            if(!ProjectExists(Project.Id))
                return NotFound();

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

        private bool ProjectExists(Guid id)
        {
            return _services.Projects.GetByIdAsync(Project.Id).Result != null;
        }
    }
}
