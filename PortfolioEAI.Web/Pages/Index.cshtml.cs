using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using PortfolioEAI.Web.Models;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Web.Pages;

public class IndexModel : PageModel
{
    // Mediator for handling queries and commands
    private readonly IMediator _services;

    // Logger for logging information and errors
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IMediator services, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _services = services;
    }
    
    // Model for the Accueil page
    public AccueilModel? AccueilModel { get; set; } = default;

    /// <summary>
    /// Handles the GET request for the Index page.
    /// </summary>
    /// <returns></returns>
    public async Task OnGetAsync()
    {
        try
        {
            ModelState.Clear();
            var adminUserResult = await _services.Send(new GetAllUsersQuery());
            
            if (!adminUserResult.IsSuccess)
            {
                _logger.LogWarning(adminUserResult.Info);
                //TODO: Redirection to setup first page.
                return;
            }

            ///TODO: Remove First() when multiple admin users are supported.
            var user = await _services.Send(new GetUserByIdQuery(adminUserResult.Value!.First()));

            if (!user.IsSuccess)
            {
                _logger.LogWarning(user.Info);
                //TODO: Redirection to setup first page.
                return;
            }
            AccueilModel = new AccueilModel(user.Value!);
        }
        catch (Exception)
        {
            RedirectToPage("/Errors/Error");
        }
    }
}
