using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class IndexModel : PageModel
    {
        // Dependency injection for the mediator and logger
        private readonly IMediator _services;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IMediator services, ILogger<IndexModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        public SkillIndexModel SkillIndexModel { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                // Clear any existing model state errors before fetching data
                ModelState.Clear();
                var result = await _services.Send(new GetAllSkillsQuery(SkillIndexModel.Query));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return;
                }
                
                SkillIndexModel.SkillModels.AddRange(result.Value!.Select(dto => new SkillModel(dto)));
                _logger.LogInformation(result.Info, result.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving skills: {ex.Message}");
                _logger.LogError(ex, "An error occurred while retrieving skills.");
            }
        }
    }
}
