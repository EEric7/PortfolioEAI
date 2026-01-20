using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class DetailsUserModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserDto User { get; set; } = default!;
    }
}