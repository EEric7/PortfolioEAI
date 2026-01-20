using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord
{
    public class HomeModel : PageModel
    {
        private readonly IMediator _services;

        public HomeModel(IMediator services)
        {
            _services = services;
            UserHomeModel = new UserHomeModel();
        }
        
        [BindProperty]
        public UserHomeModel UserHomeModel { get; set; } 

        public async Task OnGetAsync(string Email)
        {
            try
            {
                ModelState.Clear();
                //TODO Replace with actual user identification logic.
                var result = await _services.Send(new GetUserByEmailQuery(Email));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Error!);
                    return;
                }

                UserHomeModel.User = result.Value;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
            }
        }
    }
}