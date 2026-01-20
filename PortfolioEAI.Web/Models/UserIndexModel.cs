using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserIndexModel : AMenuDashbordModel
    {
        [BindProperty]
        public List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}