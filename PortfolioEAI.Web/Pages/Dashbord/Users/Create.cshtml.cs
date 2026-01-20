using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _services;

        public CreateModel(IMediator services)
        {
            _services = services;
            UserCreateModel = new UserCreateModel();
        }

        // Bind the UserCreateModel property to the page.
        [BindProperty]
        public UserCreateModel UserCreateModel { get; set; }

        public IActionResult OnGet()
        {
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

                // Call the service to create the user.
                var result = await _services.Send(new CreateUserCommand(UserCreateModel?.User.Email, UserCreateModel?.User.UserName, UserCreateModel?.User.Password, UserCreateModel?.User.Roles));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating user: {result.Error}");
                    return Page();
                }

                // Redirect to the users list page upon successful creation.
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the user: {ex.Message}");
                return Page();
            }
        }
    }
}
