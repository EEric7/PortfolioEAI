using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
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
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = string.Empty;

        public IList<SkillDto> Skills { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                Skills = (await _services.GetAllSkillsAsync()).ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving skills: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            try
            {
                var all = await _services.GetAllSkillsAsync();
                if (string.IsNullOrWhiteSpace(Query))
                {
                    Skills = all.ToList();
                    return Page();
                }

                var q = Query.ToLowerInvariant();
                Skills = all
                    .Where(s => !string.IsNullOrEmpty(s.Name) && s.Name.ToLowerInvariant().StartsWith(q))
                    .ToList();

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving skills: {ex.Message}");
                return Page();
            }
        }

    }
}
