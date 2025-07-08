using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
{
    public class CreateModel : PageModel
    {
        private readonly IExperienceService _experienceService;

        public CreateModel(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                if (await ExperienceExistsAsync(Experience))
                {
                    ModelState.AddModelError(string.Empty, "An experience with the same title and company already exists.");
                    return Page();
                }

                await _experienceService.AddAsync(Experience);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing experiences: {ex.Message}");
                return NotFound();
            }
        }

        private async Task<bool> ExperienceExistsAsync(ExperienceDto dto)
        {
            var experiences = await _experienceService.GetAllAsync();
            var existingExperience = experiences.FirstOrDefault(e => e.Title == dto.Title && e.Company == dto.Company);

            return existingExperience != null;
        }
    }
}
