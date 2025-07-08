using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IProjectService _servicesProjects;

        public EditModel(IProjectService servicesProjects)
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                if (!await ProjectExistsAsync(Project))
                {
                    ModelState.AddModelError(string.Empty, "The project does not exist.");
                    return RedirectToPage("./Index");
                }

                await _servicesProjects.UpdateAsync(Project);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the project: {ex.Message}");
                return Page();
            }
        }

        private async Task<bool> ProjectExistsAsync(ProjectDto dto)
        {
            var skills = await _servicesProjects.GetAllAsync();
            var existingSkills = skills.FirstOrDefault(e => e.Title == dto.Title && e.Description == dto.Description);

            return existingSkills is not null;
        }
    }
}
