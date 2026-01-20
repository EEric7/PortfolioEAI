using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Users
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _services;

        public IndexModel(IMediator services)
        {
            _services = services;
            UserIndexModel = new UserIndexModel();
        }

        [BindProperty]
        public UserIndexModel UserIndexModel { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetAllUsersQuery());

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }

                UserIndexModel.Users = result.Value!.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving users: {ex.Message}");
            }
        }
    }
}
