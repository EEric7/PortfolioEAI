using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
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

        public IActionResult OnGet()
        {
            ModelState.Clear();
            return Page();
        }

        [BindProperty]
        public SkillCreateModel SkillCreateModel { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();


                var result = await _services.Send(new CreateSkillCommand(SkillCreateModel.DTO));

                if (result.IsSuccess || result.Value == Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return Page();
                }

                //TODO: Associate the newly created skill with the user
                var resultUser = await _services.Send(new AddSkillUserCommand(Guid.Empty, result.Value));
                
                if(!resultUser.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, resultUser.Info!);
                    _logger.LogWarning(resultUser.Info, resultUser.Value);
                    return Page();
                }
                
                _logger.LogInformation(resultUser.Info, resultUser.Value);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                return NotFound();
            }
        }
    }
}
