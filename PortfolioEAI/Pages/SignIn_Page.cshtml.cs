using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioEAI.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PortfolioEAI.Pages
{
    public class SignIn_Page : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }
        
        private readonly ILogger<SignIn_Page> _logger;

        public SignIn_Page(ILogger<SignIn_Page> logger)
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