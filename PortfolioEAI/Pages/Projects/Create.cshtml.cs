using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IGenericServices<ProjectDto> _servicesProjectDto;

        public CreateModel(IGenericServices<ProjectDto> servicesProjectDto)
        {
            _servicesProjectDto = servicesProjectDto;
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

            await _servicesProjectDto.AddAsync(Project);

            return RedirectToPage("./Index");
        }
    }
}
