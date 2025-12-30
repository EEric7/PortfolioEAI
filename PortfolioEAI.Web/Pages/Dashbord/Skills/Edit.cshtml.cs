using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class EditModel : PageModel
    {
        private readonly IDashbordService _services;

        public EditModel(IDashbordService services)
        {
            _services = services;
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        [BindProperty]
        public SkillDto Skill { get; set; } = default!;

        [BindProperty]
        public IEnumerable<SkillLevel>? Levels { get; set; }

        [BindProperty]
        public IEnumerable<SkillCategory>? Categorys { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var skill =  await _services.GetSkillByIdAsync(id);
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

                if (!await _services.SkillExistsAsync(Skill))
                {
                    ModelState.AddModelError(string.Empty, "The skill does not exist.");
                    return RedirectToPage("./Index");
                }
                await _services.UpdateSkillAsync(Skill);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                return NotFound();
            }
        }
    }
}
