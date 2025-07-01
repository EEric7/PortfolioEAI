using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IServices _services;

        public CreateModel(IServices services)
        {
            _services = services;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _services.Projects.AddAsync(Project);

            return RedirectToPage("./Index");
        }
    }
}
