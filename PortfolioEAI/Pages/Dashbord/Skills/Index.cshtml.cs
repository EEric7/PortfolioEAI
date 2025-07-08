using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Skills
{
    public class IndexModel : PageModel
    {
        private readonly ISkillService _skillService;

        public IndexModel(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public IList<SkillDto> Skills { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                var skills = await _skillService.GetAllAsync();
                Skills = skills.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving skills: {ex.Message}");
            }
        }
    }
}
