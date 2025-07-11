using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Models;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages;

public class IndexModel : PageModel
{
    private readonly IService _services;

    private ILogger<IndexModel> _logger;

    public IndexModel(IService services, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _services = services;
    }
    
    private AdminUserDto? AdminUser { get; set; } = default;

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
            var adminUser = (await _services.AdminUserService.GetAllAsync()).ToList().FirstOrDefault();

            if (adminUser != null)
                AccueilModel = new AccueilModel(adminUser);
            else
            {
                _logger.LogWarning("AdminUser not found.");
                RedirectToPage("/Errors/Error404");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving AdminUser: {ex.Message}");
            RedirectToPage("/Errors/Error");
        }
    }
}
