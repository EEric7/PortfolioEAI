using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserEditModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserDto User { get; set; } = default!;
    }
}