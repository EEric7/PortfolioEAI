using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class DeleteModel : PageModel
    {
        private readonly IDashbordService _services;

        public DeleteModel(IDashbordService services)
        {
            _services = services;
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Projects", "/Dashbord/Projects/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        [BindProperty]
        public SkillDto Skill { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var skill = await _services.GetSkillByIdAsync(id);

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
                if (!await _services.SkillExistsAsync(Skill))
                {
                    ModelState.AddModelError(string.Empty, "The skill does not exist or has already been deleted.");
                    return Page();
                }
                await _services.DeleteSkillAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the skill: {ex.Message}");
                return NotFound();
            }
        }
    }
}
