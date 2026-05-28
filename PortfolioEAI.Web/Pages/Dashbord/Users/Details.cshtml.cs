using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class DetailsModel : PageModel
    {
        // Handles operations
        private readonly IMediator _services;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IMediator services, ILogger<DetailsModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public DetailsUserModel DetailsUserModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                //Fetch the admin user by ID
                ModelState.Clear();
                var result = await _services.Send(new GetUserByIdQuery(id));

                // Check if the result is successful
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                // Map the result to the DetailsUserModel
                DetailsUserModel.UserModel.DTO = result.Value!;
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                _logger.LogError(ex, "An error occurred while retrieving the admin user.");
                return NotFound();
            }
        }
    }
}
