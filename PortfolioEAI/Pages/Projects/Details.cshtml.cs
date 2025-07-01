using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IGenericServices<ProjectDto> _servicesProjectDto;

        public DetailsModel(IGenericServices<ProjectDto> servicesProjectDto)
        {
            _servicesProjectDto = servicesProjectDto;
        }

        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var project = await _servicesProjectDto.GetByIdAsync(id);

            if (project is not null)
            {
                Project = project;

                return Page();
            }
            
            return NotFound();
        }
    }
}
