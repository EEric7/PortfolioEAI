
using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserDeleteModel : AMenuDashbordModel
    {
        public UserDeleteModel() {}

        [BindProperty]
        public UserDto? User { get; set; }
    }
}