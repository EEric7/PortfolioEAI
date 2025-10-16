using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
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
        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var experience = await _services.GetExperienceByIdAsync(id);

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
                if (!await _services.ExperienceExistsAsync(id))
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }

                await _services.DeleteExperienceAsync(id);
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
