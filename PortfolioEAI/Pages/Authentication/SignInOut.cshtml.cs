using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Models;

namespace PortfolioEAI.Pages.Authentication
{
    public class SignInOutModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        private readonly ILogger<SignInOutModel> _logger;
        
        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Page Vitrine", "/Index"),
            new Tuple<string, string>("Sign In", "/Dashbord/Home")
        };

        public SignInOutModel(ILogger<SignInOutModel> logger)
        {
            _logger = logger;
        }

        public void OnGet() { }

         public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Input.UserName == "admin" && Input.Password == "password")
            {
                // var claims = new List<Claim>
                // {
                //     new Claim(ClaimTypes.Name, Input.Username)
                // };
                // var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                // var principal = new ClaimsPrincipal(identity);

                // await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}