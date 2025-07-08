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
            try
            {
                var project = await _servicesProjects.GetByIdAsync(id);

                if (project is null)
                {
                    ModelState.AddModelError(string.Empty, "Project not found.");
                    return RedirectToPage("./Index");
                }

                Project = project;
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the project details.");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try 
            {
                var project = await _servicesProjects.GetByIdAsync(id);

                if (project is null)
                {
                    ModelState.AddModelError(string.Empty, "Project not found.");
                    return RedirectToPage("./Index");
                }

                await _servicesProjects.DeleteAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the project: {ex.Message}");
                return NotFound();
            }
        }
    }
}
