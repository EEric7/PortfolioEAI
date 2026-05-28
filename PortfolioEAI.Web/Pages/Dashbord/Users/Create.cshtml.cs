using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
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

        // Bind the UserCreateModel property to the page.
        [BindProperty]
        public UserCreateModel UserCreateModel { get; set; } = new();

        public IActionResult OnGet()
        {
            ModelState.Clear();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Validate the model state.
                if (!ModelState.IsValid)
                    return Page();

                // Handle file storage if a photo file is provided.
                if(UserCreateModel.UserModel.DTO != null)
                    UserCreateModel.UserModel.DTO.ProfilePhoto = await UserCreateModel.UserModel.StoredFiles();

                // Call the service to create the user.
                var result = await _services.Send(new CreateUserCommand(UserCreateModel!.UserModel!.DTO!));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating user: {result.Info}");
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }

                // Redirect to the users list page upon successful creation.
                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the user: {ex.Message}");
                _logger.LogError(ex, "Error creating user");
                return Page();
            }
        }
    }
}
