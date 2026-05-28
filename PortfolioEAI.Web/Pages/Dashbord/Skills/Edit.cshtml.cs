using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class EditModel : PageModel
    {
        private readonly IMediator _services;

        private readonly ILogger<EditModel> _logger;

        public EditModel(IMediator services, ILogger<EditModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        public SkillEditeModel SkillEdite { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Clear any existing model state errors before fetching data
                ModelState.Clear();

                // Fetch the skill details using the provided ID
                var result =  await _services.Send(new GetSkillQuery(id));
                if (!result.IsSuccess || result.Value is null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                // Populate the SkillModel with the retrieved data
                SkillEdite.SkillModel = new SkillModel(result.Value);
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the skill: {ex.Message}");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Validate the model state before attempting to update the skill
                if (!ModelState.IsValid)
                    return Page();

                // Send the update command to the mediator with the data from the form
                var result = await _services.Send(new UpdateSkillCommand(SkillEdite.SkillModel.DTO!));
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                // If the update is successful, log the information and redirect to the index page
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                _logger.LogWarning(ex.Message, ex);
                return NotFound();
            }
        }
    }
}
