using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class EditModel : PageModel
    {
        private readonly ISkillService _skillService;

        public EditModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [BindProperty]
        public SkillDto Skill { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var skill =  await _skillService.GetByIdAsync(id);

                if (skill is null)
                {
                    ModelState.AddModelError(string.Empty, "Skill not found.");
                    return Page();
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                if (!await SkillExistsAsync(Skill))
                {
                    ModelState.AddModelError(string.Empty, "The skill does not exist.");
                    return RedirectToPage("./Index");
                }

                await _skillService.UpdateAsync(Skill);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
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
