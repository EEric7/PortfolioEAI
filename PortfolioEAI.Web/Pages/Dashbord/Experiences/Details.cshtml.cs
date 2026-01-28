using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class DetailsModel : PageModel
    {
        // Dependencies
        private readonly IMediator _services;
        private readonly ILogger<DetailsModel> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public DetailsModel(IMediator services, ILogger<DetailsModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public DetailsExperienceModel DetailsExperienceModel { get; set; } = new DetailsExperienceModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                ModelState.Clear();
                // Attempt to retrieve the experience by ID
                var result = await _services.Send(new GetExperienceQuery(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    _logger.LogError(result.Info!);
                    return RedirectToPage("./Index");
                }

                DetailsExperienceModel.DTO = result.Value!;
                _logger.LogInformation("Experience retrieved successfully: {ExperienceId}", id);
                return Page();
            }
            catch (Exception ex)
            {
                string msg = "An error occurred while retrieving the experience details. : " + ex.Message;
                ModelState.AddModelError(string.Empty, msg);
                _logger.LogError(msg);
                return NotFound();
            }
        }
    }
}
