using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Experiences.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IDashbordService _services;

        public DeleteModel(IDashbordService services)
        {
            _services = services;
        }

        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Dashbord", "/Dashbord/Home"),
            new Tuple<string, string>("Experiences", "/Dashbord/Experiences/"),
            new Tuple<string, string>("Skills", "/Dashbord/Skills/"),
            new Tuple<string, string>("Setting", "/Dashbord/AdminUsers/"),
            new Tuple<string, string>("SignOut", "/Authentication/SignInOut")
        };

        [BindProperty]
        public ProjectDto Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var project = await _services.GetProjectByIdAsync(id);

                if (project is null)
                {
                    ModelState.AddModelError(string.Empty, "Project not found.");
                    return RedirectToPage("./Index");
                }

                Project = project;
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the project details.");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try 
            {
                var project = await _services.GetProjectByIdAsync(id);

                if (project is null)
                {
                    ModelState.AddModelError(string.Empty, "Project not found.");
                    return RedirectToPage("./Index");
                }

                await _services.DeleteProjectAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the project: {ex.Message}");
                return NotFound();
            }
        }
    }
}
