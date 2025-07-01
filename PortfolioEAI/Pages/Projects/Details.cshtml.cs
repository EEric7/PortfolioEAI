using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;

namespace PortfolioEAI.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IServices _services;

        public DetailsModel(IServices services)
        {
            _services = services;
        }

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
    }
}
