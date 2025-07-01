using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly IGenericServices<ProjectDto> _servicesProjectDto;

        public EditModel(IGenericServices<ProjectDto> servicesProjectDto)
        {
            _servicesProjectDto = servicesProjectDto;
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var project =  await _servicesProjectDto.GetByIdAsync(id);
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
                await _servicesProjectDto.UpdateAsync(Project);
            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(Guid id)
        {
            return _servicesProjectDto.GetByIdAsync(Project.Id).Result != null;
        }
    }
}
