using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _services;

        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IMediator services, ILogger<DetailsModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public SkillDetailsModel SkillDetailsModel { get; set; } = new SkillDetailsModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var result = await _services.Send(new GetSkillQuery(id));

                if (!result.IsSuccess || result.Value is null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                // Populate the SkillDetailsModel with the retrieved data
                SkillDetailsModel.DTO = result.Value;
                _logger.LogInformation(result.Info, result.Value);

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the skill: {ex.Message}");
                _logger.LogWarning(ex.Message, ex);
                return NotFound();
            }
        }
    }
}
