using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class DetailsModel : PageModel
    {
        private readonly IDashbordService _services;

        public DetailsModel(IDashbordService services)
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

        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Attempt to retrieve the experience by ID
                var experienceDto = await _services.GetExperienceByIdAsync(id);

                if (experienceDto is null)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }

                Experience = experienceDto;
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the experience details.");
                return NotFound();
            }
        }
    }
}
