using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _services;

        public IndexModel(IMediator services)
        {
            _services = services;
            SkillIndexModel = new SkillIndexModel();
        }

        [BindProperty]
        public SkillIndexModel SkillIndexModel { get; set; } 

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
