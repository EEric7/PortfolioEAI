using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
{
    public class DeleteModel : PageModel
    {
        private readonly IExperienceService _experienceService;

        public DeleteModel(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [BindProperty]
        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var experience = await _experienceService.GetByIdAsync(id);

                if (experience is null)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }

                await _experienceService.DeleteAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the experience: {ex.Message}");
                return NotFound();
            }
        }
    }
}
