using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IService _servicesProjects;

        public CreateModel(IService servicesProjects)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _servicesProjects.AddAsync(Project);

            return RedirectToPage("./Index");
        }
    }
}
