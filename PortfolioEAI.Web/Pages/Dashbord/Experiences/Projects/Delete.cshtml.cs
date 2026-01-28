using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Experiences.Projects
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
        public ProjectDeleteModel ProjectDeleteModel { get; set; } = default!;

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

                ProjectDeleteModel.DTO = result.Value!;
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the project details.");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try 
            {
                var result = await _services.Send(new DeleteProjectCommand(id));

                if (!result.IsSuccess || result.Value == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Project not found.");
                    return Page();
                }
                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the project: {ex.Message}");
                return NotFound();
            }
        }
    }
}
