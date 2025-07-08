using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class DeleteModel : PageModel
    {
        private readonly ISkillService _skillService;

        public DeleteModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [BindProperty]
        public SkillDto Skill { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var skill = await _skillService.GetByIdAsync(id);

                if (skill is null)
                {
                    ModelState.AddModelError(string.Empty, "Skill not found.");
                    return RedirectToPage("./Index");
                }

                Skill = skill;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the skill: {ex.Message}");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                bool existingDto = await SkillExistsAsync(Skill);
                if (!existingDto)
                {
                    ModelState.AddModelError(string.Empty, "The skill does not exist or has already been deleted.");
                    return Page();
                }
                await _skillService.DeleteAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the skill: {ex.Message}");
                return NotFound();
            }
        }
        
        private async Task<bool> SkillExistsAsync(SkillDto dto)
        {
            var skills = await _skillService.GetAllAsync();
            var existingSkills = skills.FirstOrDefault(e => e.Name == dto.Name && e.Id == dto.Id);

            return existingSkills is not null;
        }
    }
}
