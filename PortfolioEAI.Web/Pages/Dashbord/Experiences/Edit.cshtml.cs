using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
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

        [BindProperty]
        public EditeExperienceModel EditeExperienceModel { get; set; } = new EditeExperienceModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Attempt to retrieve the experience by ID
                var result = await _services.Send(new GetExperienceQuery(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }
                
                EditeExperienceModel.Dto = result.Value!;
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                string msg = $"An error occurred while retrieving experience with ID {id}: {ex.Message}";
                ModelState.AddModelError(string.Empty, msg);
                _logger.LogError(msg);
                return NotFound();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                var result = await _services.Send(new UpdateExperienceCommand(EditeExperienceModel.Dto));

                if(!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info);
                    return Page();
                }

                _logger.LogWarning(result.Info);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                string msg = $"An error occurred while updating experience: {ex.Message}";
                ModelState.AddModelError(string.Empty, msg);
                _logger.LogError(msg);
                return NotFound();
            }
        }
    }
}
