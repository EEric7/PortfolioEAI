using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Models;

namespace PortfolioEAI.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly Services.IServices _services;

        public IndexModel(Services.IServices services)
        {
            _services = services;
        }

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Project = (IList<Project>) await _services.Projects.GetAllAsync();
        }
    }
}
