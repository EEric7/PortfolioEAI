using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class DetailsModel : PageModel
    {
         private readonly IMediator _services;

        public DetailsModel(IMediator services)
        {
            _services = services;
            DetailsUserModel = new DetailsUserModel();
        }

        [BindProperty]
        public DetailsUserModel DetailsUserModel { get; set; } 

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

                DetailsUserModel.User = result.Value!;
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
