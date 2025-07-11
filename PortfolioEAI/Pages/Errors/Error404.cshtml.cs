using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PortfolioEAI.Pages;

public class Error404Model : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public Error404Model(){ }

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
