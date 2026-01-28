using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class ExperienceModel : PageModel
    {
        private readonly IMediator _services;
        private readonly ILogger<ExperienceModel> _logger;

        public ExperienceModel(IMediator services, ILogger<ExperienceModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public IndexExperienceModel IndexExperienceModel { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetAllExperiencesQuery());

                if(!result.IsSuccess || result.Value is null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                }

                IndexExperienceModel.DTOs = result.Value!.ToList();
                _logger.LogInformation(result.Info!, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving experiences: {ex.Message}");
                _logger.LogWarning(ex, "An error occurred while retrieving experiences.");
                return NotFound();
            }
        }
    }
}
