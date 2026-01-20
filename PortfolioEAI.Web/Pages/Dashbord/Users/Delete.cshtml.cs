using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _services;

        public DeleteModel(IMediator services)
        {
            _services = services;
            UserDeleteModel = new UserDeleteModel();
        }

        [BindProperty]
        public UserDeleteModel UserDeleteModel { get; set; }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Get the admin user by ID
                var result = await _services.Send(new GetUserByIdQuery(id));

                // Check if the admin user exists
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return Page();
                }

                // Set the User property
                UserDeleteModel.User = result.Value!;

                //Delete the admin user
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                return NotFound();
            }
        }
    }
}
 