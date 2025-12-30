using PortfolioEAI.Web.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHomepageService _services;

    private ILogger<IndexModel> _logger;

    public IndexModel(IHomepageService services, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _services = services;
    }
    
    public AccueilModel AccueilModel { get; set; } = new AccueilModel();

    [BindProperty]
    public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("About", "#about"),
        new Tuple<string, string>("Skills", "#skills"),
        new Tuple<string, string>("Projects", "#projects"),
        new Tuple<string, string>("Contacts", "#contacts")
    };

    /// <summary>
    /// Handles the GET request for the Index page.
    /// </summary>
    /// <returns></returns>
    public async Task OnGetAsync()
    {
        try
        {
            ModelState.Clear();
            var adminUser = (await _services.GetAdminUser()).ToList();
            if (adminUser.Count == 0)
            {
                _logger.LogWarning("No admin user found.");
                //TODO: Redirection to setup first page.
                return;
            }

            AccueilModel = new AccueilModel(adminUser.First());
        }
        catch (Exception)
        {
            RedirectToPage("/Errors/Error");
        }
    }
}
