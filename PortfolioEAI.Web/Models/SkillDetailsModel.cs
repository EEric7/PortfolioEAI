using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class SkillDetailsModel : AMenuDashbordModel
    {
        public SkillDto DTO { get; set; } = default!;
    }
}