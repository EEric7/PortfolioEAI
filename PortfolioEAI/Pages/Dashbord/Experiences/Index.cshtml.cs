using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Dashbord.Experiences
{
    public class ExperienceModel : PageModel
    {
        private readonly IExperienceService _experiencesService;

        public ExperienceModel(IExperienceService experiencesService)
        {
            _experiencesService = experiencesService;
        }

        public IList<ExperienceDto> ExperiencesDto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                var experiences = await _experiencesService.GetAllAsync();
                ExperiencesDto = experiences.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving experiences: {ex.Message}");
            }
        }
    }
}
