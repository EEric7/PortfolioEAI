using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Experiences.Projects
{
    public class EditModel : PageModel
    {
         private readonly IMediator _services;

        private readonly ILogger<DetailsModel> _logger;

        public EditModel(IMediator services, ILogger<DetailsModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public ProjectUpdateModel ProjectUpdateModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetProjectQuery(id));

                if (!result.IsSuccess || result.Value == null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                _logger.LogInformation(result.Info, result.Value);
                ProjectUpdateModel.DTO = result.Value!;
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the project details.");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                var result = await _services.Send(new UpdateProjectCommand(ProjectUpdateModel.DTO));

                if (!result.IsSuccess || result.Value == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }

                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the project: {ex.Message}");
                return Page();
            }
        }
    }
}
