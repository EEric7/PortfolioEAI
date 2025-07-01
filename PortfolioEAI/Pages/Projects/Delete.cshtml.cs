using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;

namespace PortfolioEAI.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IServices _services;

        public DeleteModel(IServices services)
        {
            _services = services;
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var project = await _services.Projects.GetByIdAsync(id);

            if (project is not null)
            {
                Project = project;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var project = await _services.Projects.GetByIdAsync(id);

            if (project != null)
            {
                Project = project;
                await _services.Projects.DeleteAsync(Project.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
