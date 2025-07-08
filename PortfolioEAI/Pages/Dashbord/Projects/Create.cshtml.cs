using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IProjectService _servicesProjects;

        public CreateModel(IProjectService servicesProjects)
        {
            _servicesProjects = servicesProjects;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                
                if (await ProjectExistsAsync(Project))
                {
                    ModelState.AddModelError(string.Empty, "A project with the same title and description already exists.");
                    return Page();
                }

                await _servicesProjects.AddAsync(Project);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the project: {ex.Message}");
                return NotFound();
            }
        }

        private async Task<bool> ProjectExistsAsync(ProjectDto dto)
        {
            var experiences = await _servicesProjects.GetAllAsync();
            var existingExperience = experiences.FirstOrDefault(e => e.Title == dto.Title && e.Description == dto.Description);

            return existingExperience != null;
        }
    }
}
