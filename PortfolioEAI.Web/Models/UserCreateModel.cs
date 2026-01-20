using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserCreateModel : AMenuDashbordModel
    {
        public UserCreateModel() {}

        [BindProperty]
        public UserDto User { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> RoleOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "DEV", Text = "Developer" },
            new SelectListItem { Value = "CdP", Text = "Chef de Projet" },
            new SelectListItem { Value = "AdmR", Text = "Administrateur Reseaux" },
            new SelectListItem { Value = "AdmS", Text = "Administrateur Systeme" },
            new SelectListItem { Value = "AdmA", Text = "Administrateur Applicatif" },
            new SelectListItem { Value = "AdmD", Text = "Administrateur Donnees" },
            new SelectListItem { Value = "AdmC", Text = "Administrateur Cloud" },
            new SelectListItem { Value = "AdmSec", Text = "Administrateur Securite" },
        };

        public UserCreateModel(UserDto? dto)
        {
            if (dto == null) {
                throw new ArgumentNullException(nameof(dto));
            }
            User = dto!;
        }
    }
}