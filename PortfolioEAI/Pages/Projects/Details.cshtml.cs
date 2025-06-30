using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Models;

namespace PortfolioEAI.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly Services.IServices _services;

        public DetailsModel(Services.IServices services)
        {
            _services = services;
        }

        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _services.Projects.GetByIdAsync((int)id);

            if (project is not null)
            {
                Project = project;

                return Page();
            }

            return NotFound();
        }
    }
}
