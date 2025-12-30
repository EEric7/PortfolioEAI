using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
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

        public IActionResult OnGet()
        {
            return Page();
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
        public ExperienceDto Experience { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                if (await _services.ExperienceExistsAsync(Experience))
                {
                    ModelState.AddModelError(string.Empty, "An experience with the same title and company already exists.");
                    return Page();
                }

                if (PhotoFile != null && _photoService.IsValidPhotoFile(PhotoFile))
                {
                    string photoUrl = await _photoService.UploadPhotoAsync(PhotoFile, "uploads/experiences");
                    Experience.ImageUrl = photoUrl;
                }

                await _services.AddExperienceAsync(Experience);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing experiences: {ex.Message}");
                return NotFound();
            }
        }
    }
}
