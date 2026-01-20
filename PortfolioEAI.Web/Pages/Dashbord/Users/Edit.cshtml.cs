using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class EditModel : PageModel
    {
        private readonly IMediator _services;

        public EditModel(IMediator services)
        {
            _services = services;
            UserEditeModel = new UserEditModel();
        }

        [BindProperty]
        public UserEditModel UserEditeModel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                //Fetch the admin user by ID
                var result = await _services.Send(new GetUserByIdQuery(id));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return RedirectToPage("./Index");
                }

                UserEditeModel.User = result.Value!;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the admin user: {ex.Message}");
                return NotFound();
            }
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //Fetch the admin user by ID
                var result = await _services.Send(new UpdateUserCommand(UserEditeModel.User));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found.");
                    return RedirectToPage("./Index");
                }

                UserEditeModel.User = result.Value!;
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
