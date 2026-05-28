using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserCreateModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserModel UserModel { get; set; } = new(){ DTO = new UserDto(), PhotoFile = new FormFile(Stream.Null, 0, 0, string.Empty, string.Empty) };

        [BindProperty]
        public List<SelectListItem> RoleOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Admin" },
            new SelectListItem { Value = "2", Text = "User" },
            new SelectListItem { Value = "3", Text = "Visitor" }
        };
    }
}