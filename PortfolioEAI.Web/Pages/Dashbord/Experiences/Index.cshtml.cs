using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class ExperienceModel : PageModel
    {
        private readonly IDashbordService _services;

        public ExperienceModel(IDashbordService services)
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
        public IList<ExperienceDto> ExperiencesDto { get;set; } = default!;

        public Guid focusId { get; set; } = Guid.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                ExperiencesDto = await _services.GetAllExperiencesAsync();
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving experiences: {ex.Message}");
                return NotFound();
            }
        }
    }
}
