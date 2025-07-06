using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class ProjectModel : PageModel
    {
        private readonly IService _servicesProjects;

        public ProjectModel(IService servicesProjects)
        {
            _servicesProjects = servicesProjects;
        }

        public IList<ProjectDto> Projects { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var projects = await _servicesProjects.GetAllAsync();
            Projects = projects.ToList();
        }
    }
}
