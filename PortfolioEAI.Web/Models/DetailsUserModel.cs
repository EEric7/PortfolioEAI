using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Models
{
    public class DetailsUserModel : AMenuDashbordModel
    {
        [BindProperty]
        public UserModel UserModel { get; set; } = new();
    }
}