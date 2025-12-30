using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Pages.Experiences.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IDashbordService _services;
        private readonly IPhotoService _photoService;

        public CreateModel(IDashbordService services, IPhotoService photoService)
        {
            _services = services;
            _photoService = photoService;
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

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        public Guid ExperienceId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid experienceId)
        {
            try
            {
                // Attempt to retrieve the experience by ID
                ExperienceId = (await _services.GetExperienceByIdAsync(experienceId))?.Id??Guid.Empty;

                if (ExperienceId == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }
                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving the experience details.");
                return NotFound();
            }
        }

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

                var experience = await _services.GetExperienceByIdAsync(ExperienceId);
                if (experience is null)
                {
                    ModelState.AddModelError(string.Empty, "The associated experience was not found.");
                    return Page();
                }

                if (PhotoFile != null && _photoService.IsValidPhotoFile(PhotoFile))
                {
                    string photoUrl = await _photoService.UploadPhotoAsync(PhotoFile, "uploads/projects");
                    Project.ImageUrl = photoUrl;
                }

                experience.Projects.Add(Project);
                await _services.UpdateExperienceAsync(experience);
                
                TempData["SuccessMessage"] = "Projet créé avec succès.";
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
