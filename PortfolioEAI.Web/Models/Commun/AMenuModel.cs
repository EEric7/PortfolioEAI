using Microsoft.AspNetCore.Mvc;

namespace PortfolioEAI.Web.Models
{
    public abstract class AMenuModel
    {
        [BindProperty]
        public List<Tuple<string, string>> MenuModel { get; set; } = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("About", "#about"),
            new Tuple<string, string>("Skills", "#skills"),
            new Tuple<string, string>("Projects", "#projects"),
            new Tuple<string, string>("Contacts", "#contacts")
        };
    }
}