using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class DetailsModel : PageModel
    {
        private readonly ISkillService _skillService;

        public DetailsModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

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
    }
}
