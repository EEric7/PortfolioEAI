using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class DeleteModel : PageModel
    {
        // Services
        private readonly IMediator _services;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IMediator services, ILogger<DeleteModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public UserDeleteModel UserDeleteModel { get; set; } = new();
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Get the admin user by ID
                ModelState.Clear();
                var result = await _services.Send(new GetUserByIdQuery(id));

                // Check if the admin user exists
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }

                // Set the User property
                UserDeleteModel.UserModel.DTO = result.Value!;
                _logger.LogInformation(result.Info, result.Value);

                //Delete the admin user
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
 