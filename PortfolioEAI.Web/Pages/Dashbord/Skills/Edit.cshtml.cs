using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Pages.Dashbord.Skills
{
    public class EditModel : PageModel
    {
        private readonly IMediator _services;

        private readonly ILogger<EditModel> _logger;

        public EditModel(IMediator services, ILogger<EditModel> logger)
        {
            _services = services;
            _logger = logger;
        }

        [BindProperty]
        public SkillEditeModel SkillEditeModel { get; set; } = new SkillEditeModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                ModelState.Clear();
                
                var result =  await _services.Send(new GetSkillQuery(id));
                if (!result.IsSuccess || result.Value is null)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }

                SkillEditeModel.DTO = result.Value;
                _logger.LogInformation(result.Info, result.Value);

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving the skill: {ex.Message}");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                var result = await _services.Send(new UpdateSkillCommand(SkillEditeModel.DTO));

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.Info!);
                    _logger.LogWarning(result.Info, result.Value);
                    return RedirectToPage("./Index");
                }
            
                _logger.LogInformation(result.Info, result.Value);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while checking for existing skills: {ex.Message}");
                _logger.LogWarning(ex.Message, ex);
                return NotFound();
            }
        }
    }
}
