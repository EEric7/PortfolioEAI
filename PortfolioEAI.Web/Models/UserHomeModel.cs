using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserHomeModel : AMenuModel
    {
        [BindProperty]
        public UserDto? User { get; set; }
    }
}