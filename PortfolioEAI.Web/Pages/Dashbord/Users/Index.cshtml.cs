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

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IMediator services, ILogger<IndexModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public UserIndexModel UserIndexModel { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                ModelState.Clear();
                var result = await _services.Send(new GetAllUsersQuery());

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    _logger.LogWarning(result.Info, result.Value); return;  
                }

                UserIndexModel.UserModels.Clear();
                UserIndexModel.UserModels.AddRange(result.Value!.Select(dto => new UserModel { DTO = dto }));
                _logger.LogInformation(result.Info, result.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving users: {ex.Message}");
                _logger.LogError(ex, "An error occurred while retrieving users.");
            }
        }
    }
}
