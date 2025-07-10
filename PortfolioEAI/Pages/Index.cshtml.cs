using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
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
    public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    public async Task OnGetAsync()
    {
        try
        {
            ModelState.Clear();
            AdminUser = (await _services.AdminUserService.GetAllAsync()).ToList().FirstOrDefault();
            if (AdminUser != null)
            {
                foreach (var experience in AdminUser.Experiences)
                {
                    if (experience.Projects != null && experience.Projects.Any())
                    {
                        Projects.AddRange(experience.Projects);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred while retrieving AdminUser: {ex.Message}");
        }
    }
}
