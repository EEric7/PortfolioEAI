using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class CreateModel : PageModel
    {
        private readonly ISkillService _skillService;

        public CreateModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SkillDto Skill { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool existingSkill = await SkillExistsAsync(Skill);
            if (existingSkill)
            {
                ModelState.AddModelError(string.Empty, "A skill with the same name already exists.");
                return Page();
            }
            try
            {
                await _skillService.AddAsync(Skill);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                return NotFound();
            }
        }

        private async Task<bool> SkillExistsAsync(SkillDto dto)
        {
            var experiences = await _skillService.GetAllAsync();
            var existingExperience = experiences.FirstOrDefault(e => e.Name == dto.Name);
            return existingExperience != null;
        }
    }
}
