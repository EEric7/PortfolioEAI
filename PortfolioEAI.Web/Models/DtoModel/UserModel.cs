using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserModel : APhotoFileModel
    {
        [BindProperty]
        public UserDto? DTO { get; set; } = default;
    }
}