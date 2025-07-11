using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Models;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages;

public class IndexModel : PageModel
{
    private readonly IService _services;

    public IndexModel(IService services)
    {
        _services = services;
    }
    
    public AdminUserDto? AdminUser { get; set; } = default;

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
                ModelState.AddModelError(string.Empty, "AdminUser not found.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
        }
    }
}
