using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class ProjectModel : PageModel
    {
        private readonly IGenericServices<ProjectDto> _servicesProjects;

        public ProjectModel(IGenericServices<ProjectDto> services)
        {
            _servicesProjects = services;
        }

        public IList<ProjectDto> Projects { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Projects = (IList<ProjectDto>) await _servicesProjects.GetAllAsync();
        }
    }
}
