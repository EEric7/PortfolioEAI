using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserIndexModel : AMenuDashbordModel
    {
        [BindProperty]
        public List<UserDto> DTOs { get; set; } = new List<UserDto>();
    }
}