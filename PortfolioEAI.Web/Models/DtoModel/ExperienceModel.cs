using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class ExperienceModel : APhotoFileModel
    {
        [BindProperty]
        public ExperienceDto? DTO { get; set;} = default;
    }
}