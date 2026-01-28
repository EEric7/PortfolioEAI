using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _services;

        private ILogger<DeleteModel> _logger;

        public DeleteModel(IMediator services, ILogger<DeleteModel> logger) 
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public ExperienceDeleteModel ExperienceDeleteModel { get; set; } = new ExperienceDeleteModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var result = await _services.Send(new GetExperienceQuery(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogError(result.Info!);
                    return RedirectToPage("./Index");
                }
                
                // Populate the ExperienceDeleteModel with the retrieved experience details
                ExperienceDeleteModel.Experience = result.Value!;

                _logger.LogInformation(result.Info!, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                string msg = $"An error occurred while retrieving the experience details: {ex.Message}";
                _logger.LogError(msg);
                ModelState.AddModelError(string.Empty, msg);
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var result = await _services.Send(new DeleteExperienceCommand(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogError(result.Info!);
                    return RedirectToPage("./Index");
                }

                _logger.LogInformation(result.Info!, result.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                string msg = $"An error occurred while deleting the experience: {ex.Message}";
                _logger.LogError(msg);
                ModelState.AddModelError(string.Empty, msg);
                return NotFound();
            }
        }
    }
}
