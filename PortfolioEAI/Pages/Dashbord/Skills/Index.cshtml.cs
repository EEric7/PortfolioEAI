using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class IndexModel : PageModel
    {
        private readonly IDashbordService _services;

        public IndexModel(IDashbordService services)
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

        public IList<SkillDto> Skills { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                var skills = await _services.GetAllSkillsAsync();
                Skills = skills.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving skills: {ex.Message}");
            }
        }
    }
}
