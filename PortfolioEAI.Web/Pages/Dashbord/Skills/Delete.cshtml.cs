using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _services;

        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IMediator services, ILogger<DeleteModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public SkillDeleteModel SkillDeleteModel { get; set; } = new SkillDeleteModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var result = await _services.Send(new DeleteSkillCommand(id));

                if (!result.IsSuccess || result.Value == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the skill: {ex.Message}");
                _logger.LogInformation(ex.Message);
                return NotFound();
            }
        }
    }
}
