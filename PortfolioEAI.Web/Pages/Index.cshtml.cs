using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Application.Models;

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
    public AccueilModel AccueilModel { get; set; } = new AccueilModel();

    /// <summary>
    /// Handles the GET request for the Index page.
    /// </summary>
    /// <returns></returns>
    public async Task OnGetAsync()
    {
        try
        {
            ModelState.Clear();
            var result = await _services.Send(new GetAllUsersQuery());
            
            if (!result.IsSuccess || !result.Value!.Any())
            {
                _logger.LogWarning(result.Info);
                RedirectToPage("SignInOut");
                return;
            }

            // Set the user model in the AccueilModel
            AccueilModel.SetUserModel(result.Value!.First());
        }
        catch (Exception)
        {
            RedirectToPage("/Errors/Error");
        }
    }
}
