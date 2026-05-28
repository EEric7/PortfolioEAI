using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Models
{
    public class UserEditModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserModel UserModel { get; set; } = new();
    }
}