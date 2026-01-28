using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Experiences.Projects
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
        public ProjectDetailsModel ProjectDetailsModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetProjectQuery(id));

                if (!result.IsSuccess || result.Value == null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    return RedirectToPage("./Index");
                }

                _logger.LogInformation(result.Info, result.Value);
                ProjectDetailsModel.DTO = result.Value!;           
                
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the project details.");
                return NotFound();
            }
        }
    }
}
