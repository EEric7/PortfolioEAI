using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IDashbordService _services;

        public CreateModel(IDashbordService services)
        {
            _services = services;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Projects", "/Dashbord/Projects/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                
                if (await ProjectExistsAsync(Project))
                {
                    ModelState.AddModelError(string.Empty, "A project with the same title and description already exists.");
                    return Page();
                }

                await _services.AddProjectAsync(Project);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the project: {ex.Message}");
                return NotFound();
            }
        }

        private async Task<bool> ProjectExistsAsync(ProjectDto dto)
        {
            var projects = await _services.GetAllProjectsAsync();
            var existingProject = projects.FirstOrDefault(e => e.Title == dto.Title && e.Description == dto.Description);

            return existingProject != null;
        }
    }
}
