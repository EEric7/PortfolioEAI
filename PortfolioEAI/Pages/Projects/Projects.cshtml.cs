using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;

namespace PortfolioEAI.Pages.Projects
{
    public class ProjectModel : PageModel
    {
        private readonly IServices _services;

        public ProjectModel(IServices services)
        {
            _services = services;
        }

        public IList<ProjectDto> Projects { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Projects = (IList<ProjectDto>) await _services.Projects.GetAllAsync();
        }
    }
}
