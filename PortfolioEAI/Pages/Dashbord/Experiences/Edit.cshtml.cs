using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
{
    public class EditModel : PageModel
    {
        private readonly IExperienceService _experienceService;

        public EditModel(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [BindProperty]
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while update experiences: {ex.Message}");
                return NotFound();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                if (!ExperienceExists(Experience.Id))
                    return NotFound();

                await _experienceService.UpdateAsync(Experience);
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
               return NotFound();
            }
        }

        private bool ExperienceExists(Guid id)
        {
            return _experienceService.GetByIdAsync(id) != null;
        }
    }
}
