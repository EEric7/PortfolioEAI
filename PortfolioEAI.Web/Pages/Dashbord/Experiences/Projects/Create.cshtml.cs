using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Experiences.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _services;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IMediator services, ILogger<CreateModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public ProjectCreateModel ProjectCreateModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid experienceId)
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetExperienceQuery(experienceId));

                if(!result.IsSuccess || result.Value == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to load experience details.");
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");   
                }

                ProjectCreateModel.ExperienceDTO = result.Value;
                _logger.LogInformation(result.Info, result.Value);

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the experience details: " + ex.Message);
                _logger.LogError(ex, "An error occurred while retrieving the experience details.");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                ProjectCreateModel.ExperienceDTO!.Projects.Add(ProjectCreateModel.DTO!);
                var result = await _services.Send(new UpdateExperienceCommand(ProjectCreateModel.ExperienceDTO));

                if (!result.IsSuccess || result.Value == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }

                //TODO: Get ID of created project and handle photo upload
                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index", new { experienceId = result.Value });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the project: {ex.Message}");
                return NotFound();
            }
        }
    }
}
