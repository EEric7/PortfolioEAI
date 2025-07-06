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
            var project =  await _servicesProjects.GetByIdAsync(id);
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
                await _servicesProjects.UpdateAsync(Project);
            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(Guid id)
        {
            return _servicesProjects.GetByIdAsync(Project.Id).Result != null;
        }
    }
}
