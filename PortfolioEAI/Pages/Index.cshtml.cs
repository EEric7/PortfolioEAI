using PortfolioEAI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Pages;

public class IndexModel : PageModel
{
    private readonly IHomepageService _services;

    private ILogger<IndexModel> _logger;

    public IndexModel(IHomepageService services, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _services = services;
    }

    [BindProperty]
    public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("About", "#about"),
        new Tuple<string, string>("Skills", "#skills"),
        new Tuple<string, string>("Projects", "#projects"),
        new Tuple<string, string>("Contacts", "#contacts")
    };

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
            var adminUser = (await _services.GetAdminUser()).ToList().FirstOrDefault();

            if (adminUser != null)
                AccueilModel = new AccueilModel(adminUser);
            else
                RedirectToPage("/Errors/Error404");
        }
        catch (Exception)
        {
            RedirectToPage("/Errors/Error");
        }
    }
}
