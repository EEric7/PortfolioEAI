using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Models
{
    public class UserIndexModel : AMenuDashbordModel
    {
        [BindProperty]
        public List<UserModel> UserModels { get; set; } = new();
    }
}