using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IService _servicesProjects;

        public DetailsModel(IService servicesProjects)
        {
            _servicesProjects = servicesProjects;
        }

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
    }
}
