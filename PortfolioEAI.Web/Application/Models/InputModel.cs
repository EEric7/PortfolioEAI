using System.ComponentModel.DataAnnotations;

namespace PortfolioEAI.Web.Application.Models
{
    public class InputModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}