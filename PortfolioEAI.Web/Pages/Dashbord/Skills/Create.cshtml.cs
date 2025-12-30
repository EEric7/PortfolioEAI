using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class CreateModel : PageModel
    {
        private readonly IDashbordService _services;

        public CreateModel(IDashbordService services)
        {
            _services = services;
        }

        public IActionResult OnGet()
        {
            return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                
                if (!await _services.SkillExistsAsync(Skill))
                {
                    ModelState.AddModelError(string.Empty, "A skill with the same name already exists.");
                    return Page();
                }

                await _services.AddSkillAsync(Skill);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                return NotFound();
            }
        }
    }
}
