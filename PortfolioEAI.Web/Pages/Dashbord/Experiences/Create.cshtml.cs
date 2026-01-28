using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Experiences
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _services;

        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IMediator services, ILogger<CreateModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public ExperienceCreateModel ExperienceCreateModel { get; set; } = new ExperienceCreateModel();

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                //TODO: Set the PhotoFile if exists

                var result = await _services.Send(new AddExpUserCommand(ExperienceCreateModel.UserId, ExperienceCreateModel.Experience));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogError(result.Info!);
                    return Page();
                }
                    
                _logger.LogInformation(result.Info, result.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                string msg = $"An error occurred while checking for existing experiences: {ex.Message}";
                ModelState.AddModelError(string.Empty, msg);
                _logger.LogError(msg);
                return NotFound();
            }
        }
    }
}
