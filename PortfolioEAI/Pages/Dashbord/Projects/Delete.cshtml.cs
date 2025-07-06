using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IProjectService _servicesProjects;

        public DeleteModel(IProjectService servicesProjects)
        {
            _servicesProjects = servicesProjects;
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var project = await _servicesProjects.GetByIdAsync(id);

            if (project is not null)
            {
                Project = project;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var project = await _servicesProjects.GetByIdAsync(id);

            if (project != null)
            {
                Project = project;
                await _servicesProjects.DeleteAsync(Project.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
