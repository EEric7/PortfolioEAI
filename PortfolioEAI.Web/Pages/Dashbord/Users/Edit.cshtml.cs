using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class EditModel : PageModel
    {
        // Handles operations
        private readonly IMediator _services;
        private readonly ILogger<EditModel> _logger;

        /// <summary>
        /// Constructor to initialize services and logger.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        public EditModel(IMediator services, ILogger<EditModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public UserEditModel UserEditeModel { get; set; } = new UserEditModel();

        /// <summary>
        /// Fetch the admin user details for editing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                //Fetch the admin user by ID
                var result = await _services.Send(new GetUserByIdQuery(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                _logger.LogInformation(result.Info, result.Value);
                UserEditeModel.DTO = result.Value!;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                _logger.LogError(ex, "An error occurred while retrieving the admin user.");
                return NotFound();
            }
        }
        
        /// <summary>
        /// Update the admin user details.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //Fetch the admin user by ID
                var result = await _services.Send(new UpdateUserCommand(UserEditeModel.DTO));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }
                
                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index");
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
