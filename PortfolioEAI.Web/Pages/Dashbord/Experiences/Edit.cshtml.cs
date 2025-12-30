using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Application.Services.Interfaces;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class EditModel : PageModel
    {
        private readonly IDashbordService _services;

        public EditModel(IDashbordService services)
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
        public ExperienceDto Experience { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                // Attempt to retrieve the experience by ID
                var experience = await _services.GetExperienceByIdAsync(id);

                if (experience is null)
                {
                    ModelState.AddModelError(string.Empty, "Experience not found.");
                    return RedirectToPage("./Index");
                }
                
                Experience = experience;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while update experiences: {ex.Message}");
                return NotFound();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                return Page();

                if (await _services.ExperienceExistsAsync(Experience))
                    return NotFound();

                await _services.UpdateExperienceAsync(Experience);
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
               return NotFound();
            }
        }
    }
}
