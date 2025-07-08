using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
{
    public class DetailsModel : PageModel
    {
        private readonly IExperienceService _experienceService;

        public DetailsModel(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Attempt to retrieve the experience by ID
                var experience = await _experienceService.GetByIdAsync(id);

                if (experience is null)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }

                Experience = experience;
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the experience details.");
                return NotFound();
            }
        }
    }
}
