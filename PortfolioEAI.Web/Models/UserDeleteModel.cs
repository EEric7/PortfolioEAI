using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserDeleteModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserModel UserModel { get; set; } = new(){ PhotoFile = new FormFile(Stream.Null, 0, 0, string.Empty, string.Empty) };
    }
}